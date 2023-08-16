using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Data.Excel.Common;
using Weedwacker.GameServer.Database;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.GameServer.Systems.Inventory;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;
using static Weedwacker.GameServer.Data.Excel.WeaponData;

namespace Weedwacker.GameServer.Systems.Avatar;

internal class Avatar
{
    [BsonElement] public int BornTime { get; private set; }
    [BsonElement] public uint AvatarId { get; private set; }           // Id of avatar
    [BsonIgnore] public Player.Player Owner { get; private set; } // Loaded by DatabaseManager
    [BsonIgnore] public AvatarCompiledData Data => GameServer.AvatarInfo[AvatarId];
    [BsonIgnore] public ulong Guid { get; private set; }           // Player unique Avatar id. Generated each session
    [BsonIgnore] public uint EntityId => AsEntity.EntityId;

    [BsonElement] public uint Level { get; set; } = 1;
    public uint Exp;
    [BsonElement] public uint PromoteLevel { get; set; } = 0;
    public int Satiation; // ?
    public int SatiationPenalty; // ?
    [BsonElement] public LifeState LifeState { get; private set; } = LifeState.LIFE_ALIVE;
    public HashSet<string> EquipOpenConfigs; // openConfigs from relics and weapon TODO
    [BsonElement] public uint CurrentDepotId { get; private set; }
    [BsonSerializer(typeof(UIntDictionarySerializer<SkillDepot>))]
    [BsonElement] public Dictionary<uint, SkillDepot> SkillDepots { get; private set; } = new();
    [BsonIgnore] public SkillDepot CurSkillDepot => SkillDepots[CurrentDepotId];

    [BsonElement] public FetterSystem Fetters { get; private set; }
    [BsonIgnore] public Dictionary<EquipType, EquipItem> Equips { get; private set; } = new(); // Loaded through the inventory system
    [BsonIgnore] public WeaponItem? Weapon => (WeaponItem?)Equips.GetValueOrDefault(EquipType.EQUIP_WEAPON);
    [BsonElement] public Dictionary<FightPropType, float> FightProp { get; private set; } = new();
    [BsonElement] public HashSet<string> AppliedOpenConfigs { get; private set; } = new();

    [BsonElement] public uint FlyCloak { get; set; } = 140001;
    [BsonElement] public uint Costume { get; private set; } = default; // no costume

    [BsonIgnore] public AvatarEntity AsEntity;

    public static Task<Avatar> CreateAsync(uint avatarId, Player.Player owner)
    {
        Avatar? ret = new();
        return ret.InitializeAsync(avatarId, owner);
    }
    private async Task<Avatar> InitializeAsync(uint avatarId, Player.Player owner)
    {
        AvatarId = avatarId;
        Guid = owner.GetNextGameGuid();

        if (Data.GeneralData.candSkillDepotIds != null && Data.GeneralData.candSkillDepotIds.Length > 0)
        {
            foreach (uint depotId in Data.AbilityHashMap.Keys)
            {
                SkillDepots.Add(depotId, new SkillDepot(this, depotId, owner));
            }
        }
        else
        {
            SkillDepots.Add(Data.GeneralData.skillDepotId, new SkillDepot(this, Data.GeneralData.skillDepotId, owner));
        }
        CurrentDepotId = Data.GeneralData.skillDepotId;
        Fetters = await FetterSystem.CreateAsync(this, owner);
        Owner = owner;
        BornTime = (int)(DateTimeOffset.Now.ToUnixTimeMilliseconds() / 1000);

        // Combat properties
        foreach (FightPropType property in (FightPropType[])Enum.GetValues(typeof(FightPropType)))
        {
            if ((int)property > 0 && (int)property < 3000) FightProp.Add(property, 0f);
        }

        // Set stats
        await RecalcStatsAsync();
        await SetCurrentHp(FightProp[FightPropType.FIGHT_PROP_MAX_HP]);

        GameItem? weapon = Owner.Inventory.AddItemByIdAsync(Data.GeneralData.initialWeapon).Result;
        await EquipWeapon((WeaponItem)weapon, false);

        return this;
    }

    internal Avatar Clone()
    {
        throw new NotImplementedException();
    }

    public async Task OnLoadAsync(Player.Player owner)
    {
        Owner = owner;
        Guid = owner.GetNextGameGuid();
        List<Task>? tasks = new()
        {
            Fetters.OnLoadAsync(owner, this)
        };
        foreach (SkillDepot depot in SkillDepots.Values)
        {
            tasks.Add(depot.OnLoadAsync(owner, this));
        }
        await Task.WhenAll(tasks);
    }
    public async Task<bool> SetCurrentHp(float value, bool notifyClient = true)
    {
        if (!FightProp.TryGetValue(FightPropType.FIGHT_PROP_MAX_HP, out float maxHp))
            return false;

        value = Math.Min(value, maxHp);
        FightProp[FightPropType.FIGHT_PROP_CUR_HP] = value;

        // Update
        FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
        UpdateDefinition<AvatarManager>? update = Builders<AvatarManager>.Update.Set($"{nameof(AvatarManager.Avatars)}.{AvatarId}.{nameof(FightProp)}.{FightPropType.FIGHT_PROP_CUR_HP}", value);
        await DatabaseManager.UpdateAvatarsAsync(filter, update);
        if (notifyClient) await Owner.SendPacketAsync(new PacketAvatarFightPropUpdateNotify(this, FightPropType.FIGHT_PROP_CUR_HP));
        return true;
    }
    public async Task<bool> SetFlyCloakAsync(uint cloakId)
    {
        FlyCloak = cloakId;
        FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
        UpdateDefinition<AvatarManager>? update = Builders<AvatarManager>.Update.Set($"{nameof(AvatarManager.Avatars)}.{AvatarId}.{FlyCloak}", FlyCloak);
        await DatabaseManager.UpdateAvatarsAsync(filter, update);
        await Owner.SendPacketAsync(new PacketAvatarFlycloakChangeNotify(this));

        return true;
    }

    static public int GetMinPromoteLevel(int level)
    {
        if (level > 80)
        {
            return 6;
        }
        else if (level > 70)
        {
            return 5;
        }
        else if (level > 60)
        {
            return 4;
        }
        else if (level > 50)
        {
            return 3;
        }
        else if (level > 40)
        {
            return 2;
        }
        else if (level > 20)
        {
            return 1;
        }
        return 0;
    }

    public GameItem? GetRelic(EquipType slot)
    {
        if (slot == EquipType.EQUIP_WEAPON)
        {
            Logger.WriteErrorLine("Tried to access weapon as relic");
            return null;
        }
        return Equips[slot];
    }

    public float GetCurrentEnergy()
    {
        if (CurSkillDepot.Element != null)
            return CurSkillDepot.Element.CurEnergy;
        else
            return 0;
    }
    public async Task<bool> SetCurrentEnergy(float currentEnergy, bool update = true)
    {
        if (CurSkillDepot.Element == null) return false;

        currentEnergy = Math.Min(currentEnergy, CurSkillDepot.Element.MaxEnergy);
        CurSkillDepot.Element.CurEnergy = currentEnergy;
        FightProp[CurSkillDepot.Element.CurEnergyProp] = currentEnergy;
        FightProp[CurSkillDepot.Element.MaxEnergyProp] = CurSkillDepot.Element.MaxEnergy;

        // false = used in RecalcStatsAsync or is in Tower team
        if (!update) return true;

        // Update
        FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
        UpdateDefinition<AvatarManager>? updateQuery = Builders<AvatarManager>.Update.Set($"{nameof(AvatarManager.Avatars)}.{AvatarId}.{nameof(SkillDepots)}.{CurrentDepotId}.{nameof(CurSkillDepot.Element)}.{nameof(CurSkillDepot.Element.CurEnergy)}", currentEnergy);
        await DatabaseManager.UpdateAvatarsAsync(filter, updateQuery);

        await Owner.SendPacketAsync(new PacketAvatarFightPropUpdateNotify(this, CurSkillDepot.Element.CurEnergyProp));

        return true;

    }

    public async Task<bool> AddToFightProperty(FightPropType prop, float value, bool update = true)
    {
        FightProp[prop] = FightProp.GetValueOrDefault(prop) + value;

        // false = used in RecalcStatsAsync
        if (update)
        {
            // Update
            FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
            UpdateDefinition<AvatarManager>? updateQuery = Builders<AvatarManager>.Update.Set($"{nameof(AvatarManager.Avatars)}.{AvatarId}.{nameof(FightProp)}.{prop}", FlyCloak);
            await DatabaseManager.UpdateAvatarsAsync(filter, updateQuery);
            await Owner.SendPacketAsync(new PacketAvatarFightPropUpdateNotify(this, prop));
            return true;
        }
        return true;
    }

    internal async Task SetFightProperty(FightPropType prop, float value)
    {
        FightProp[prop] = value;
        await RecalcStatsAsync(reloadBaseProps: false);
    }

    public async Task<bool> EquipRelic(ReliquaryItem item, bool notifyClient = true)
    {
        // Sanity check equip type
        EquipType itemEquipType = item.ItemData.equipType;
        if (itemEquipType == EquipType.EQUIP_NONE)
        {
            return false;
        }

        else if (GetRelic(itemEquipType) != null)
        {
            // Unequip item in current slot if it exists
            await UnequipRelic(itemEquipType);
        }

        // Set equip
        Equips[itemEquipType] = item;
        item.EquippedAvatar = AvatarId;

        //TODO add relic openConfigs

        // Update Database
        FilterDefinition<InventoryManager>? inventoryFilter = Builders<InventoryManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
        UpdateDefinition<InventoryManager>? inventoryUpdate = Builders<InventoryManager>.Update.Set($"{nameof(InventoryManager.SubInventories)}.{ItemType.ITEM_RELIQUARY}.{nameof(RelicTab.Items)}.{item.Id}.{nameof(EquipItem.EquippedAvatar)}", item.EquippedAvatar);
        await DatabaseManager.UpdateInventoryAsync(inventoryFilter, inventoryUpdate);

        if (Owner.HasSentLoginPackets)
        {
            await Owner.SendPacketAsync(new PacketAvatarEquipChangeNotify(this, item, itemEquipType));
        }

        await RecalcStatsAsync(notifyClient);

        return true;
    }
    public async Task<bool> EquipWeapon(WeaponItem item, bool notifyClient = true)
    {
        if (Weapon != null)
        {
            // Unequip item in current slot if it exists
            await UnequipWeapon(notifyClient);
        }

        // Set equip
        Equips[EquipType.EQUIP_WEAPON] = item;
        item.EquippedAvatar = AvatarId;
        if (Owner.World != null)
            item.WeaponEntityId = Owner.World.GetNextEntityId(EntityIdType.WEAPON);

        //TODO apply weapon openConfigs

        // Update Database
        FilterDefinition<InventoryManager>? inventoryFilter = Builders<InventoryManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
        UpdateDefinition<InventoryManager>? inventoryUpdate = Builders<InventoryManager>.Update.Set($"{nameof(InventoryManager.SubInventories)}.{ItemType.ITEM_WEAPON}.{nameof(WeaponTab.Items)}.{item.Id}.{nameof(WeaponItem.EquippedAvatar)}", item.EquippedAvatar);
        await DatabaseManager.UpdateInventoryAsync(inventoryFilter, inventoryUpdate);

        if (Owner.HasSentLoginPackets && notifyClient)
        {
            await Owner.SendPacketAsync(new PacketAvatarEquipChangeNotify(this, item, EquipType.EQUIP_WEAPON));
        }

        await RecalcStatsAsync(notifyClient);

        return true;
    }

    public async Task<bool> UnequipRelic(EquipType slot, bool notifyClient = true)
    {
        ReliquaryItem item = (ReliquaryItem)Equips[slot];
        Equips.Remove(slot);

        //TODO remove relic openConfigs


        if (item != null)
        {
            item.EquippedAvatar = 0;

            // Update Database
            FilterDefinition<InventoryManager>? inventoryFilter = Builders<InventoryManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
            UpdateDefinition<InventoryManager>? inventoryUpdate = Builders<InventoryManager>.Update.Set($"{nameof(InventoryManager.SubInventories)}.{ItemType.ITEM_RELIQUARY}.{nameof(RelicTab.Items)}.{item.Id}.{nameof(ReliquaryItem.EquippedAvatar)}", item.EquippedAvatar);
            await DatabaseManager.UpdateInventoryAsync(inventoryFilter, inventoryUpdate);

            await Owner.SendPacketAsync(new PacketAvatarEquipChangeNotify(this, slot));
            await RecalcStatsAsync(notifyClient);
            return true;
        }

        return false;
    }
    public async Task<bool> UnequipWeapon(bool notifyClient = true)
    {
        WeaponItem item = (WeaponItem)Equips[EquipType.EQUIP_WEAPON];
        Equips.Remove(EquipType.EQUIP_WEAPON);

        //TODO remove weapon openConfigs

        if (item != null)
        {
            item.EquippedAvatar = 0;

            // Update Database
            FilterDefinition<InventoryManager>? inventoryFilter = Builders<InventoryManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
            UpdateDefinition<InventoryManager>? inventoryUpdate = Builders<InventoryManager>.Update.Set($"{nameof(InventoryManager.SubInventories)}.{ItemType.ITEM_WEAPON}.{nameof(WeaponTab.Items)}.{item.Id}.{nameof(WeaponItem.EquippedAvatar)}", item.EquippedAvatar);
            await DatabaseManager.UpdateInventoryAsync(inventoryFilter, inventoryUpdate);

            if (notifyClient) await Owner.SendPacketAsync(new PacketAvatarEquipChangeNotify(this, EquipType.EQUIP_WEAPON));
            await RecalcStatsAsync(notifyClient);
            return true;
        }

        return false;
    }

    public async Task RecalcStatsAsync(bool forceSendAbilityChange = false, bool reloadBaseProps = true)
    {
        // Setup
        AvatarPromoteData promoteData = Data.PromoteData[PromoteLevel];

        // Get hp percent, set to 100% if none
        float hpPercent = FightProp[FightPropType.FIGHT_PROP_MAX_HP] == 0 ? 1f : FightProp.GetValueOrDefault(FightPropType.FIGHT_PROP_CUR_HP) / FightProp.GetValueOrDefault(FightPropType.FIGHT_PROP_MAX_HP);

        // Store current energy value for later
        float currentEnergy = GetCurrentEnergy();

        // Store old values to reset them if base props should not be reset
        float baseHp = FightProp[FightPropType.FIGHT_PROP_BASE_HP];
        float baseAtk = FightProp[FightPropType.FIGHT_PROP_BASE_ATTACK];
        float baseDef = FightProp[FightPropType.FIGHT_PROP_BASE_DEFENSE];
        float crit = FightProp[FightPropType.FIGHT_PROP_CRITICAL];
        float critHurt = FightProp[FightPropType.FIGHT_PROP_CRITICAL_HURT];
        float chargeEff = FightProp[FightPropType.FIGHT_PROP_CHARGE_EFFICIENCY];
        float maxHp = FightProp[FightPropType.FIGHT_PROP_MAX_HP];
        float curAtk = FightProp[FightPropType.FIGHT_PROP_CUR_ATTACK];
        float curDef = FightProp[FightPropType.FIGHT_PROP_CUR_DEFENSE];
        float curHp = FightProp[FightPropType.FIGHT_PROP_CUR_HP];

        // Clear properties
        FightProp.Clear();
        if (CurSkillDepot.Element != null)
            CurSkillDepot.Element.CurEnergy = 0;

        // Base stats
        FightProp[FightPropType.FIGHT_PROP_BASE_HP] = reloadBaseProps ? Data.GetBaseHp(Level) : baseHp;
        FightProp[FightPropType.FIGHT_PROP_BASE_ATTACK] = reloadBaseProps ? Data.GetBaseAttack(Level) : baseAtk;
        FightProp[FightPropType.FIGHT_PROP_BASE_DEFENSE] = reloadBaseProps ? Data.GetBaseDefense(Level) : baseDef;
        FightProp[FightPropType.FIGHT_PROP_CRITICAL] = reloadBaseProps ? Data.BaseCritical : crit;
        FightProp[FightPropType.FIGHT_PROP_CRITICAL_HURT] = reloadBaseProps ? Data.BaseCriticalHurt : critHurt;
        FightProp[FightPropType.FIGHT_PROP_CHARGE_EFFICIENCY] = reloadBaseProps ? 1f : chargeEff;

        foreach (PropValConfig fightPropData in promoteData.addProps)
        {
            if (fightPropData.propType != null && fightPropData.value != null)
                FightProp[(FightPropType)fightPropData.propType] = FightProp.GetValueOrDefault((FightPropType)fightPropData.propType) + (float)fightPropData.value;
        }

        // Set energy usage
        await SetCurrentEnergy(currentEnergy, false);

        // Artifacts

        // artifact set ids and number of artifacts
        Dictionary<uint, uint> sets = new();

        for (int slotId = 1; slotId <= 5; slotId++)
        {
            // Get artifact

            if (!Equips.TryGetValue((EquipType)slotId, out EquipItem equip))
            {
                continue;
            }
            // Artifact main stat
            ReliquaryMainPropData mainPropData = GameData.ReliquaryMainPropDataMap[(equip as ReliquaryItem).MainPropId];
            if (mainPropData != null)
            {
                ReliquaryLevelData levelData = GameData.ReliquaryLevelDataMap[Tuple.Create((uint)equip.ItemData.rank, equip.Level)];
                if (levelData != null)
                {
                    FightProp[mainPropData.propType] = FightProp.GetValueOrDefault(mainPropData.propType) + (levelData.addProps.Where(w => w.propType == mainPropData.propType).First().value ?? 0);
                }
            }
            // Artifact sub stats
            if ((equip as ReliquaryItem).AppendPropIdList != null)
            {
                foreach (uint appendPropId in (equip as ReliquaryItem).AppendPropIdList)
                {
                    ReliquaryAffixData affixData = GameData.ReliquaryAffixDataMap[appendPropId];
                    if (affixData != null)
                    {
                        FightProp[affixData.propType] = FightProp.GetValueOrDefault(affixData.propType) + affixData.propValue;
                    }
                }
            }
            // Set bonus
            if ((equip as ReliquaryItem).ItemData.setId != null)
            {
                sets[(uint)(equip as ReliquaryItem).ItemData.setId] += 1;
            }
        }

        // Artifact Set stuff
        foreach (KeyValuePair<uint, uint> e in sets.ToList())
        {
            ReliquarySetData setData = GameData.ReliquarySetDataMap[e.Key];
            if (setData == null)
            {
                continue;
            }

            // Calculate how many items are from the set
            uint amount = e.Value;

            // Add affix data from set bonus
            for (int setIndex = 0; setIndex < setData.setNeedNum.Length; setIndex++)
            {
                if (amount >= setData.setNeedNum[setIndex])
                {
                    uint affixId = (uint)((setData.EquipAffixId * 10) + setIndex);

                    EquipAffixData affix = GameData.EquipAffixDataMap[affixId];
                    if (affix == null)
                    {
                        continue;
                    }

                    // Add properties from this affix to our avatar
                    foreach (PropValConfig prop in affix.addProps)
                    {
                        if (prop.propType == null || prop.value == null) continue;
                        FightProp[(FightPropType)prop.propType] = FightProp.GetValueOrDefault((FightPropType)prop.propType) + (float)prop.value;
                    }
                }
                else
                {
                    break;
                }
            }
        }

        // Weapon
        WeaponItem weapon = Weapon;
        if (weapon != null)
        {
            // Add stats
            WeaponCurveData curveData = GameData.WeaponCurveDataMap[weapon.Level];
            if (curveData != null)
            {
                if (weapon.ItemData.weaponProp != null)
                {
                    foreach (WeaponProperty weaponProperty in weapon.ItemData.weaponProp)
                    {
                        if (weaponProperty.propType == FightPropType.FIGHT_PROP_NONE) continue;
                        FightProp[weaponProperty.propType] = FightProp.GetValueOrDefault(weaponProperty.propType) + WeaponCurveData.CalcValue(weaponProperty.initValue, curveData.GetArith(weaponProperty.type));
                    }
                }
            }
            // Weapon promotion stats
            WeaponPromoteData wepPromoteData = GameData.WeaponPromoteDataMap[Tuple.Create(weapon.ItemData.weaponPromoteId, weapon.PromoteLevel)];
            if (wepPromoteData != null)
            {
                foreach (PropValConfig prop in wepPromoteData.addProps)
                {
                    if (prop.value == null || prop.propType == null) continue;
                    FightProp[(FightPropType)prop.propType] = FightProp.GetValueOrDefault((FightPropType)prop.propType) + (float)prop.value;
                }
            }
            // Add weapon skill from affixes
            if (weapon.Affixes != null && weapon.Affixes.Count > 0)
            {
                // Weapons usually dont have more than one affix but just in case...
                foreach (int af in weapon.Affixes)
                {
                    if (af == 0)
                    {
                        continue;
                    }
                    // Calculate affix id
                    uint affixId = (uint)((af * 10) + weapon.Refinement);
                    EquipAffixData affix = GameData.EquipAffixDataMap[affixId];
                    if (affix == null)
                    {
                        continue;
                    }

                    // Add properties from this affix to our avatar
                    foreach (PropValConfig prop in affix.addProps)
                    {
                        if (prop.propType == null || prop.value == null) continue;
                        FightProp[(FightPropType)prop.propType] = FightProp.GetValueOrDefault((FightPropType)prop.propType) + (float)prop.value;
                    }
                }
            }
        }

        // Proud skills
        foreach (ProudSkillData proudSkill in CurSkillDepot.InherentProudSkillOpens)
        {
            // Add properties from this proud skill to our avatar
            if (proudSkill.addProps != null)
            {
                foreach (PropValConfig prop in proudSkill.addProps)
                {
                    if (prop.propType == null || prop.value == null) continue;
                    FightProp[(FightPropType)prop.propType] = FightProp.GetValueOrDefault((FightPropType)prop.propType) + (float)prop.value;
                }
            }
        }

        // Set % stats
        FightProp[FightPropType.FIGHT_PROP_MAX_HP] = !reloadBaseProps ? maxHp :
            (FightProp[FightPropType.FIGHT_PROP_BASE_HP] * (1f + FightProp.GetValueOrDefault(FightPropType.FIGHT_PROP_HP_PERCENT))) + FightProp.GetValueOrDefault(FightPropType.FIGHT_PROP_HP);
        FightProp[FightPropType.FIGHT_PROP_CUR_ATTACK] = !reloadBaseProps ? curAtk :
            FightProp[FightPropType.FIGHT_PROP_BASE_ATTACK] * (1f + FightProp.GetValueOrDefault(FightPropType.FIGHT_PROP_ATTACK_PERCENT)) + FightProp.GetValueOrDefault(FightPropType.FIGHT_PROP_ATTACK);
        FightProp[FightPropType.FIGHT_PROP_CUR_DEFENSE] = !reloadBaseProps ? curDef :
            FightProp[FightPropType.FIGHT_PROP_BASE_DEFENSE] * (1f + FightProp.GetValueOrDefault(FightPropType.FIGHT_PROP_DEFENSE_PERCENT)) + FightProp.GetValueOrDefault(FightPropType.FIGHT_PROP_DEFENSE);

        // Set current hp
        FightProp[FightPropType.FIGHT_PROP_CUR_HP] = !reloadBaseProps ? Math.Min(curHp, maxHp) : FightProp[FightPropType.FIGHT_PROP_MAX_HP] * hpPercent;

        // Update Database
        FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
        UpdateDefinition<AvatarManager>? updateQuery = Builders<AvatarManager>.Update.Set($"{nameof(AvatarManager.Avatars)}.{AvatarId}.{nameof(FightProp)}", FightProp);
        await DatabaseManager.UpdateAvatarsAsync(filter, updateQuery);

        // Packet
        if (Owner.HasSentLoginPackets)
        {
            // Update stats for client
            await Owner.SendPacketAsync(new PacketAvatarFightPropNotify(this));
        }
    }

    public AvatarInfo ToProto()
    {
        int fetterLevel = Fetters.FetterLevel;
        AvatarFetterInfo avatarFetter = new() { ExpLevel = (uint)fetterLevel };

        if (fetterLevel != 10)
        {
            avatarFetter.ExpNumber = (uint)Fetters.FetterExp;
        }
        else
        {
            avatarFetter.RewardedFetterLevelList.Add(10);
        }
        foreach (FetterData fetter in Fetters.FetterStates.Values)
        {
            avatarFetter.FetterList.Add(fetter);
        }

        AvatarInfo avatarInfo = new()
        {
            AvatarId = AvatarId,
            Guid = Guid,
            CostumeId = (uint)Costume,
            WearingFlycloakId = (uint)FlyCloak,
            FetterInfo = avatarFetter,
            BornTime = (uint)BornTime,
            SkillDepotId = CurrentDepotId,
            LifeState = (uint)LifeState,
            AvatarType = 1,
            CoreProudSkillLevel = CurSkillDepot.CoreProudSkillLevel
        };

        foreach (ProudSkillData proud in CurSkillDepot.InherentProudSkillOpens)
        {
            avatarInfo.InherentProudSkillList.Add(proud.proudSkillId);
        }
        foreach (uint talent in CurSkillDepot.Talents)
        {
            avatarInfo.TalentIdList.Add(talent);
        }
        foreach (uint skill in CurSkillDepot.GetSkillLevelMap().Keys)
        {
            avatarInfo.SkillLevelMap.Add(skill, CurSkillDepot.GetSkillLevelMap()[skill]);
        }
        foreach (FightPropType prop in FightProp.Keys)
        {
            avatarInfo.FightPropMap.Add((uint)prop, FightProp[prop]);
        }
        foreach (uint groupId in CurSkillDepot.ProudSkillExtraLevelMap.Keys)
        {
            avatarInfo.ProudSkillExtraLevelMap.Add(groupId, CurSkillDepot.ProudSkillExtraLevelMap[groupId]);
        }
        foreach (uint skillId in CurSkillDepot.SkillExtraChargeMap.Keys)
        {
            avatarInfo.SkillMap.Add(skillId, new AvatarSkillInfo() { MaxChargeCount = CurSkillDepot.SkillExtraChargeMap[skillId] });
        }

        foreach (GameItem item in Equips.Values)
        {
            avatarInfo.EquipGuidList.Add(item.Guid);
        }

        avatarInfo.PropMap.Add((uint)PropType.PROP_LEVEL, new PropValue() { Type = (uint)PropType.PROP_LEVEL, Ival = Level, Val = Level });
        avatarInfo.PropMap.Add((uint)PropType.PROP_EXP, new PropValue() { Type = (uint)PropType.PROP_EXP, Ival = (uint)Exp, Val = (uint)Exp });
        avatarInfo.PropMap.Add((uint)PropType.PROP_BREAK_LEVEL, new PropValue() { Type = (uint)PropType.PROP_BREAK_LEVEL, Ival = (uint)PromoteLevel, Val = (uint)PromoteLevel });
        avatarInfo.PropMap.Add((uint)PropType.PROP_SATIATION_VAL, new PropValue() { Type = (uint)PropType.PROP_SATIATION_VAL, Ival = 0, Val = 0 });
        avatarInfo.PropMap.Add((uint)PropType.PROP_SATIATION_PENALTY_TIME, new PropValue() { Type = (uint)PropType.PROP_SATIATION_PENALTY_TIME, Ival = 0, Val = 0 });

        return avatarInfo;
    }

    // used only in character showcase
    public ShowAvatarInfo ToShowAvatarInfoProto()
    {
        int fetterLevel = Fetters.FetterLevel;
        AvatarFetterInfo avatarFetter = new() { ExpLevel = (uint)fetterLevel };

        ShowAvatarInfo showAvatarInfo = new()
        {
            AvatarId = AvatarId,
            CostumeId = Costume,
            SkillDepotId = CurrentDepotId,
            CoreProudSkillLevel = CurSkillDepot.CoreProudSkillLevel,
            FetterInfo = avatarFetter
        };
        foreach (uint talent in CurSkillDepot.Talents)
        {
            showAvatarInfo.TalentIdList.Add(talent);
        }
        foreach (ProudSkillData proudSkill in CurSkillDepot.InherentProudSkillOpens)
        {
            showAvatarInfo.InherentProudSkillList.Add(proudSkill.proudSkillId);
        }
        foreach (uint skill in CurSkillDepot.GetSkillLevelMap().Keys)
        {
            showAvatarInfo.SkillLevelMap.Add(skill, CurSkillDepot.GetSkillLevelMap()[skill]);
        }
        foreach (uint groupId in CurSkillDepot.ProudSkillExtraLevelMap.Keys)
        {
            showAvatarInfo.ProudSkillExtraLevelMap.Add(groupId, CurSkillDepot.ProudSkillExtraLevelMap[groupId]);
        }
        foreach (FightPropType prop in FightProp.Keys)
        {
            showAvatarInfo.FightPropMap.Add((uint)prop, FightProp[prop]);
        }


        showAvatarInfo.PropMap.Add((uint)PropType.PROP_LEVEL, new PropValue() { Type = (uint)PropType.PROP_LEVEL, Ival = (uint)Level, Val = (uint)Level });
        showAvatarInfo.PropMap.Add((uint)PropType.PROP_EXP, new PropValue() { Type = (uint)PropType.PROP_EXP, Ival = (uint)Exp, Val = (uint)Exp });
        showAvatarInfo.PropMap.Add((uint)PropType.PROP_BREAK_LEVEL, new PropValue() { Type = (uint)PropType.PROP_BREAK_LEVEL, Ival = PromoteLevel, Val = PromoteLevel });
        showAvatarInfo.PropMap.Add((uint)PropType.PROP_SATIATION_VAL, new PropValue() { Type = (uint)PropType.PROP_SATIATION_VAL, Ival = (uint)Satiation, Val = (uint)Satiation });
        showAvatarInfo.PropMap.Add((uint)PropType.PROP_SATIATION_PENALTY_TIME, new PropValue() { Type = (uint)PropType.PROP_SATIATION_VAL, Ival = (uint)SatiationPenalty, Val = (uint)SatiationPenalty });
        int maxStamina = Owner.PlayerProperties[PropType.PROP_MAX_STAMINA];
        showAvatarInfo.PropMap.Add((uint)PropType.PROP_MAX_STAMINA, new PropValue() { Type = (uint)PropType.PROP_MAX_STAMINA, Ival = (uint)maxStamina, Val = (uint)maxStamina });

        foreach (ReliquaryItem item in Equips.Values)
        {
            showAvatarInfo.EquipList.Add(new ShowEquip()
            {
                ItemId = item.ItemId,
                Reliquary = item.ToReliquaryProto()
            });
        }

        WeaponItem weapon = (WeaponItem)Equips[EquipType.EQUIP_WEAPON];
        showAvatarInfo.EquipList.Add(new ShowEquip()
        {
            ItemId = weapon.ItemId,
            Weapon = weapon.ToWeaponProto()
        });

        return showAvatarInfo;
    }
}
