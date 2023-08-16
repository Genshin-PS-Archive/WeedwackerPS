﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Data.BinOut.Talent;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Data.Excel.Common;
using Weedwacker.GameServer.Database;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Avatar
{
    internal class SkillDepot
    {
        [BsonElement] public uint DepotId { get; private set; }
        [BsonIgnore] private Player.Player Owner; // Loaded by DatabaseManager
        [BsonIgnore] private Avatar Character; // Loaded by DatabaseManager
        [BsonElement] public uint? EnergySkill { get; private set; } // Ultimate elemental ability (Q)
        [BsonElement] public uint EnergySkillLevel { get; private set; }
        [BsonElement] public ElementType? Element { get; private set; } // Stores current and max energy
        [BsonIgnore] public Dictionary<uint, ConfigAbility>? Abilities { get; private set; } // hash
        [BsonElement] public SortedList<uint, uint> Skills { get; private set; } = new(); // <skillId,level>
        [BsonElement] public SortedList<uint, uint> SubSkills { get; private set; } = new(); // <skillId,level>
        [BsonElement] public SortedList<uint, uint> SkillExtraChargeMap { get; private set; } = new(); // Charges
        [BsonIgnore] public IEnumerable<ProudSkillData> InherentProudSkillOpens => Character.Data.ProudSkillData[DepotId].Where(w => InherentProudSkillIds.Contains(w.Key)).Select(w => w.Value); // convenient to keep
        [BsonElement] public HashSet<uint> InherentProudSkillIds { get; private set; } = new();
        [BsonIgnore] public IEnumerable<ProudSkillData> TeamOpens => InherentProudSkillOpens.Where(w => w.effectiveForTeam == 1);
        [BsonElement] public HashSet<uint> Talents { get; private set; } = new(); // talentId. last digit of id = constellationRank.
        [BsonIgnore] public Dictionary<string, HashSet<string>> UnlockedTalentParams = new(); // <abilityName, talentParams> Added by ConfigTalent UnlockTalentParam
        [BsonIgnore] public Dictionary<uint, Dictionary<uint, object>?>? AbilitySpecials { get; private set; } = new(); // <abilityNameHash, <abilitySpecialHash, value>> Variables used in ConfigAbility_<Avatar>.json
        [BsonIgnore] public SortedList<uint, uint> ProudSkillExtraLevelMap { get; private set; } = new(); // <groupId,extraLevels> 
        [BsonIgnore] public uint CoreProudSkillLevel => (uint)Talents.Count;
        public SkillDepot(Avatar avatar, uint depotId, Player.Player owner)
        {
            Owner = owner;
            Character = avatar;
            DepotId = depotId;
            AvatarCompiledData? avatarInfo = GameServer.AvatarInfo[avatar.AvatarId];
            EnergySkill = avatarInfo.SkillDepotData[depotId].energySkill;
            // Hero Avatars don't have an element at the beginning
            if (EnergySkill != null)
            {
                EnergySkillLevel = 1;
                Data.Enums.ElementType type = (Data.Enums.ElementType)avatarInfo.SkillData[depotId][(uint)EnergySkill].costElemType;
                switch (type)
                {
                    case Data.Enums.ElementType.Fire:
                        Element = new Fire();
                        break;
                    case Data.Enums.ElementType.Water:
                        Element = new Water();
                        break;
                    case Data.Enums.ElementType.Wind:
                        Element = new Wind();
                        break;
                    case Data.Enums.ElementType.Ice:
                        Element = new Ice();
                        break;
                    case Data.Enums.ElementType.Rock:
                        Element = new Rock();
                        break;
                    case Data.Enums.ElementType.Electric:
                        Element = new Electric();
                        break;
                    case Data.Enums.ElementType.Grass:
                        Element = new Grass();
                        break;
                    default:
                        Logger.WriteErrorLine("Unknown Avatar Element Type");
                        break;
                }
                Element.MaxEnergy = (float)avatarInfo.SkillData[depotId][(uint)EnergySkill].costElemVal;
            }

            foreach (uint? skillId in avatarInfo.SkillDepotData[depotId].skills)
            {
                if (skillId != null) Skills.Add((uint)skillId, 1);
            }

            List<uint?>? inherentProudSkillGroups = avatarInfo.SkillDepotData[depotId].inherentProudSkillOpens.Where(w => (w.needAvatarPromoteLevel < avatar.PromoteLevel)).ToDictionary(q => q.proudSkillGroupId).Keys.ToList();
            foreach (int group in inherentProudSkillGroups)
            {
                List<uint>? idList = avatarInfo.ProudSkillData[depotId].Where(w => w.Value.proudSkillGroupId == group).ToDictionary(q => q.Key).Keys.ToList();
                foreach (uint id in idList)
                {
                    InherentProudSkillIds.Add(id);
                }
            }

            InitializeConfig();
        }

        private void InitializeConfig()
        {
            AbilitySpecials = new Dictionary<uint, Dictionary<uint, object>>();
            ProudSkillExtraLevelMap = new SortedList<uint, uint>();
            UnlockedTalentParams = new Dictionary<string, HashSet<string>>();

			if (Character.Data.AbilityHashMap.TryGetValue(DepotId, out Dictionary<uint, ConfigAbility>? hashMap))
				Abilities = hashMap;
			foreach (ConfigAbility config in hashMap.Values)
            {
                if (config.abilitySpecials != null)
                    AbilitySpecials.Add(Utils.AbilityHash(config.abilityName), config.abilitySpecials.ToDictionary(w => Utils.AbilityHash(w.Key), w => w.Value));
            }
            
        }

        public async Task OnLoadAsync(Player.Player owner, Avatar avatar)
        {
            Owner = owner;
            Character = avatar;
            InitializeConfig();
        }

        public List<uint> GetSkillsAndEnergySkill()
        {
            List<uint>? list = Skills.Values.Where(w => w > 0).ToList();
            if (EnergySkill != null)
                list.Add((uint)EnergySkill);
            return list;

        }
        public Dictionary<uint, uint> GetSkillLevelMap()
        {
            Dictionary<uint, uint> skillz = new();
            if (EnergySkill != null)
                skillz.Add((uint)EnergySkill, EnergySkillLevel);
            foreach (uint skill in Skills.Keys)
            {
                skillz.Add(skill, Skills[skill]);
            }

            return skillz;
        }

        public async Task<bool> UpgradeSkill(uint skillId)
        {
            AvatarSkillData skillData = GameServer.AvatarInfo[Character.AvatarId].SkillData[DepotId][skillId];
            if (skillData == null) return false;

            // Get data for next skill level
            uint newLevel = Skills.GetValueOrDefault<uint, uint>(skillId, 0) + 1;
            if (newLevel > 10) return false;

            // Proud skill data
            ProudSkillData proudSkill = GameServer.AvatarInfo[Character.AvatarId].ProudSkillData[DepotId]
                .Where(w => w.Value.proudSkillGroupId == skillData.proudSkillGroupId && w.Value.level == newLevel).First().Value;

            // Make sure break level is correct
            if (Character.PromoteLevel < proudSkill.breakLevel) return false;

            // Pay materials and mora if possible
            if (!await Owner.Inventory.PayPromoteCostAsync(proudSkill.GetTotalCostItems())) return false;

            // Upgrade skill        
            return await SetSkillLevel(skillId, newLevel);
        }

        internal async Task SendAvatarSkillInfoNotify()
        {
            if (!SkillExtraChargeMap.Any()) return;
            await Owner.SendPacketAsync(new PacketAvatarSkillInfoNotify(Character.Guid, SkillExtraChargeMap));
        }

        public async Task<bool> SetSkillLevel(uint skillId, uint level)
        {
            if (level < 0 || level > 15) return false;
            uint oldLevel = Skills.GetValueOrDefault<uint, uint>(skillId, 0);
            Skills[skillId] = level;

            // Update Database
            FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
            UpdateDefinition<AvatarManager>? update = Builders<AvatarManager>.Update.Set(w => w.Avatars[Character.AvatarId].SkillDepots[DepotId].Skills[skillId], level);
            await DatabaseManager.UpdateAvatarsAsync(filter, update);

            // Packet
            await Owner.SendPacketAsync(new PacketAvatarSkillChangeNotify(Character, DepotId, skillId, oldLevel, level));

            return true;
        }
        private void AddTalent(uint talentId)
        {
            AvatarTalentData? talentData = Character.Data.TalentData[DepotId][talentId];
            Talents.Add(talentData.talentId);
            foreach (ConfigTalentMixin config in Character.Data.ConfigTalentMap[DepotId][talentData.openConfig])
            {
                config.Apply(Character.AsEntity.AbilityManager, talentData.paramList);
            }
        }

        public void AddProudSkill(uint proudSkillId)
        {
            ProudSkillData? proudSkillData = Character.Data.ProudSkillData[DepotId][proudSkillId];
            InherentProudSkillIds.Add(proudSkillId);
            foreach (ConfigTalentMixin config in Character.Data.ConfigTalentMap[DepotId][proudSkillData.openConfig])
            {
                config.Apply(Character.AsEntity.AbilityManager, proudSkillData.paramList.Cast<float?>().ToArray());
            }
        }

        public async Task<bool> UnlockConstellation(uint talentId, bool skipPayment = false)
        {
            // Get talent
            AvatarTalentData talentData = GameServer.AvatarInfo[Character.AvatarId].TalentData[DepotId][talentId];
            if (talentData == null) return false;

            // Pay constellation item if possible
            if (!skipPayment && !await Owner.Inventory.PayPromoteCostAsync(new[] { new IdCountConfig { id = talentData.mainCostItemId, count = 1 } })) // don't judge...
            {
                return false;
            }

            // Apply + recalc
            AddTalent(talentId);

            // Update Database
            FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
            UpdateDefinition<AvatarManager>? update = Builders<AvatarManager>.Update.Set(w => w.Avatars[Character.AvatarId].SkillDepots[DepotId], this);
            await DatabaseManager.UpdateAvatarsAsync(filter, update);

            // Packet
            await Owner.SendPacketAsync(new PacketAvatarUnlockTalentNotify(Character, DepotId, talentId));

            // Recalc + update avatar
            await Character.RecalcStatsAsync(true);

            return true;
        }
    }
}
