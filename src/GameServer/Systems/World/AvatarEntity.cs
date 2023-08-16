using System.Numerics;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.GameServer.Systems.Ability;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.GameServer.Systems.Inventory;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;
using Vector = Weedwacker.Shared.Network.Proto.Vector;

namespace Weedwacker.GameServer.Systems.World;

internal class AvatarEntity : SceneEntity
{
    public readonly Avatar.Avatar Avatar;
    public TeamInfo TeamInfo { get; private set; }
    public uint KilledBy { get; protected set; }
    public Shared.Network.Proto.PlayerDieType KilledType { get; protected set; }
    public override Vector3 Position { get => Avatar.Owner.Position; protected set => Avatar.Owner.Position = value; }
    public override Vector3 Rotation { get => Avatar.Owner.Rotation; protected set => Avatar.Owner.Rotation = value; }
    private float CachedLandingSpeed = 0;
    private long CachedLandingTimeMillisecond = 0;
    private bool monitorLandingEvent = false;

    public AvatarEntity(TeamInfo team, Scene scene, Avatar.Avatar avatar) : base(scene)
    {
        TeamInfo = team;
        Avatar = avatar;
        LiveState = LifeState.LIFE_ALIVE;
        EntityId = scene.World.GetNextEntityId(EntityIdType.AVATAR);
        FightProps = avatar.FightProp;
        WeaponItem weapon = avatar.Weapon;
        weapon.WeaponEntityId = scene.World.GetNextEntityId(EntityIdType.WEAPON);
        AbilityManager = new AvatarAbilityManager(this);
        AbilityManager.Initialize();
        Avatar.AsEntity = this;
    }

    public AvatarEntity(TeamInfo info, ushort index) : base(null)
    {
        TeamInfo = info;
        LiveState = LifeState.LIFE_ALIVE;
        Avatar = info.AvatarInfo[index];
        FightProps = Avatar.FightProp;
    }

    public override async Task MoveAsync(EntityMoveInfo moveInfo)
    {
        await base.MoveAsync(moveInfo);
        MotionState = moveInfo.MotionInfo.State;
        LastMoveReliableSeq = moveInfo.ReliableSeq;
        LastMoveSceneTimeMs = moveInfo.SceneTime;
        //await Avatar.Owner.StaminaManager.HandleMoveInfoAsync(moveInfo);

        // TODO: handle MOTION_FIGHT landing which has a different damage factor
        // 		Also, for plunge attacks, LAND_SPEED is always -30 and is not useful.
        //  	May need the height when starting plunge attack.

        // MOTION_LAND_SPEED and MOTION_FALL_ON_GROUND arrive in different packets.
        // Cache land speed for later use.
        if (MotionState == MotionState.LandSpeed)
        {
            CachedLandingSpeed = moveInfo.MotionInfo.Speed.Y;
            CachedLandingTimeMillisecond = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            monitorLandingEvent = true;
        }
        if (monitorLandingEvent)
        {
            if (moveInfo.MotionInfo.State == MotionState.FallOnGround)
            {
                monitorLandingEvent = false;
                await HandleFallOnGround();
            }
        }
    }

    private async Task HandleFallOnGround()
    {
    }

    public override async Task OnDeathAsync(uint killerId = default, Shared.Network.Proto.PlayerDieType dieType = Shared.Network.Proto.PlayerDieType.KillByMonster)
    {
        KilledBy = killerId;
        KilledType = dieType;

        await ClearEnergy(ChangeEnergyReason.None);
    }

    public override async Task<float> HealAsync(float amount, bool mute)
    {
        // Do not heal character if they are dead
        if (FightProps[FightPropType.FIGHT_PROP_CUR_HP] <= 0f)
            return 0f;

        float healed = await base.HealAsync(amount, mute);

        if (healed > 0f)
            await Scene.BroadcastPacketAsync(new PacketEntityFightPropChangeReasonNotify(this, FightPropType.FIGHT_PROP_CUR_HP, healed, mute ? PropChangeReason.None : PropChangeReason.Ability, ChangeHpReason.AddAbility));

        return healed;
    }

    internal override async Task SetFightProperty(FightPropType prop, float value)
    {
        switch (prop)
        {
            case FightPropType.FIGHT_PROP_CUR_HP:
                await Avatar.SetCurrentHp(value);
                break;

            case FightPropType.FIGHT_PROP_CUR_ELEC_ENERGY:
            case FightPropType.FIGHT_PROP_CUR_FIRE_ENERGY:
            case FightPropType.FIGHT_PROP_CUR_GRASS_ENERGY:
            case FightPropType.FIGHT_PROP_CUR_ICE_ENERGY:
            case FightPropType.FIGHT_PROP_CUR_ROCK_ENERGY:
            case FightPropType.FIGHT_PROP_CUR_WATER_ENERGY:
            case FightPropType.FIGHT_PROP_CUR_WIND_ENERGY:
                await Avatar.SetCurrentEnergy(value);
                break;

            case FightPropType.FIGHT_PROP_MAX_ELEC_ENERGY:
            case FightPropType.FIGHT_PROP_MAX_FIRE_ENERGY:
            case FightPropType.FIGHT_PROP_MAX_GRASS_ENERGY:
            case FightPropType.FIGHT_PROP_MAX_ICE_ENERGY:
            case FightPropType.FIGHT_PROP_MAX_ROCK_ENERGY:
            case FightPropType.FIGHT_PROP_MAX_WATER_ENERGY:
            case FightPropType.FIGHT_PROP_MAX_WIND_ENERGY:
                if (Avatar.CurSkillDepot.Element != null)
                    Avatar.CurSkillDepot.Element.MaxEnergy = value;
                goto default;

            default:
                await Avatar.SetFightProperty(prop, value);
                await Scene.BroadcastPacketAsync(new PacketEntityFightPropUpdateNotify(this, prop));
                break;
        }
    }

    #region Energy management

    public async Task ClearEnergy(ChangeEnergyReason reason)
    {
        // HINT: Can happen at the start of the game, when Traveler has no element yet
        if (Avatar.CurSkillDepot.Element == null)
            return;

        // Fight props.
        FightPropType curEnergyProp = Avatar.CurSkillDepot.Element.CurEnergyProp;

        // Get max energy.
        float maxEnergy = Avatar.CurSkillDepot.Element.MaxEnergy;

        // Set energy to zero.
        await Avatar.SetCurrentEnergy(0);

        // Send packets.
        await Scene.BroadcastPacketAsync(new PacketEntityFightPropUpdateNotify(this, curEnergyProp));

        if (reason == ChangeEnergyReason.SkillStart)
        {
            await Scene.BroadcastPacketAsync(new PacketEntityFightPropChangeReasonNotify(this, curEnergyProp, -maxEnergy, reason));
        }
    }

    public async Task AddEnergyAsync(float amount, PropChangeReason reason, bool isFlat = false)
    {
        if (Avatar.CurSkillDepot.Element is null || Avatar.LifeState == LifeState.LIFE_DEAD)
            return;
        float curEnergy = Avatar.GetCurrentEnergy();
        float maxEnergy = Avatar.CurSkillDepot.Element.MaxEnergy;

        // Get energy recharge.
        float energyRecharge = Avatar.FightProp[FightPropType.FIGHT_PROP_CHARGE_EFFICIENCY];

        // Scale amount by energy recharge, if the amount is not flat.
        if (!isFlat)
        {
            amount *= energyRecharge;
        }

        // Determine the new energy value.
        float newEnergy = Math.Min(curEnergy + amount, maxEnergy);

        // Set energy and notify.
        if (newEnergy != curEnergy)
        {
            await Avatar.SetCurrentEnergy(newEnergy);

            await Scene.BroadcastPacketAsync(new PacketAvatarFightPropUpdateNotify(Avatar, Avatar.CurSkillDepot.Element.CurEnergyProp));
            await Scene.BroadcastPacketAsync(new PacketEntityFightPropChangeReasonNotify(this, Avatar.CurSkillDepot.Element.CurEnergyProp, newEnergy, reason));
        }
    }

    #endregion

    public SceneAvatarInfo GetSceneAvatarInfo()
    {
        SceneAvatarInfo avatarInfo = new()
        {
            Uid = (uint)Avatar.Owner.GameUid,
            AvatarId = (uint)Avatar.AvatarId,
            Guid = Avatar.Guid,
            PeerId = Avatar.Owner.PeerId,
            CostumeId = (uint)Avatar.Costume,
            WearingFlycloakId = (uint)Avatar.FlyCloak,
            BornTime = (uint)Avatar.BornTime,
            SkillDepotId = (uint)Avatar.CurrentDepotId,
            CoreProudSkillLevel = Avatar.CurSkillDepot.CoreProudSkillLevel,
            /* TODO
			AnimHash =,
			CurVehicleInfo =,
			EquipIdList =,
			ExcelInfo =,
			ServerBuffList =,
			*/
        };
        foreach (uint talent in Avatar.CurSkillDepot.Talents) avatarInfo.TalentIdList.Add(talent);
        foreach (KeyValuePair<uint, uint> skill in Avatar.CurSkillDepot.GetSkillLevelMap()) avatarInfo.SkillLevelMap.Add(skill.Key, skill.Value);
        foreach (uint id in Avatar.CurSkillDepot.InherentProudSkillIds) avatarInfo.InherentProudSkillList.Add(id);
        foreach (KeyValuePair<uint, uint> extra in Avatar.CurSkillDepot.ProudSkillExtraLevelMap) avatarInfo.ProudSkillExtraLevelMap.Add(extra.Key, extra.Value);
        TeamInfo.TeamResonances.AsParallel().ForAll(w => avatarInfo.TeamResonanceList.Add(w.teamResonanceId));

        foreach (EquipItem item in Avatar.Equips.Values)
        {
            if (item.ItemData.itemType == ItemType.ITEM_WEAPON)
            {
                avatarInfo.Weapon = (item as WeaponItem).CreateSceneWeaponInfo();
            }
            else
            {
                avatarInfo.ReliquaryList.Add((item as ReliquaryItem).CreateSceneReliquaryInfo());
            }
            avatarInfo.EquipIdList.Add((uint)item.ItemId);
        }

        return avatarInfo;
    }

    public override SceneEntityInfo ToProto()
    {
        //TODO
        EntityAuthorityInfo authority = new()
        {
            AbilityInfo = new AbilitySyncStateInfo(),
            RendererChangedInfo = new EntityRendererChangedInfo(),
            AiInfo = new SceneEntityAiInfo { IsAiOpen = true, BornPos = new Vector() },
            BornPos = new Vector()
        };

        //TODO
        SceneEntityInfo entityInfo = new()
        {
            EntityId = EntityId,
            EntityType = ProtEntityType.Avatar,
            EntityClientData = new EntityClientData(),
            EntityAuthorityInfo = authority,
            LastMoveSceneTimeMs = LastMoveSceneTimeMs,
            LastMoveReliableSeq = LastMoveReliableSeq,
            LifeState = (uint)LiveState,
        };
        entityInfo.AnimatorParaList.Add(new AnimatorParameterValueInfoPair());

        if (Scene != null && Avatar.Owner.TeamManager.CurrentAvatarEntity == this)
        {
            entityInfo.MotionInfo = MotionInfo;
        }
        else
        {
            entityInfo.MotionInfo = new MotionInfo { Pos = new Vector(), Rot = new Vector(), Speed = new Vector() };
        }

        foreach (KeyValuePair<FightPropType, float> entry in FightProps)
        {
            FightPropPair fightProp = new() { PropType = (uint)entry.Key, PropValue = entry.Value };
            entityInfo.FightPropList.Add(fightProp);
        }

        PropPair pair = new()
        {
            Type = (uint)PropType.PROP_LEVEL,
            PropValue = new PropValue { Type = (uint)PropType.PROP_LEVEL, Ival = Avatar.Level, Val = Avatar.Level }
        };
        entityInfo.PropList.Add(pair);

        entityInfo.Avatar = GetSceneAvatarInfo();

        return entityInfo;
    }

    public AbilityControlBlock GetAbilityControlBlock()
    {

        AbilityControlBlock abilityControlBlock = new();
        uint embryoId = 0;

        // Add team resonances
        //TODO apply properly
        foreach (TeamResonanceData? resonance in TeamInfo.TeamResonances)
        {
            AbilityEmbryo emb = new()
            {
                AbilityId = ++embryoId,
                AbilityNameHash = Utils.AbilityHash(resonance.openConfig),
                AbilityOverrideNameHash = Utils.AbilityHash("Default")
            };

            abilityControlBlock.AbilityEmbryoList.Add(emb);
        }
        // Add skill depot abilities

        foreach (int hash in Avatar.CurSkillDepot.Abilities.Keys)
        {
            AbilityEmbryo emb = new()
            {
                AbilityId = ++embryoId,
                AbilityNameHash = (uint)hash,
                AbilityOverrideNameHash = Utils.AbilityHash("Default")
            };
            abilityControlBlock.AbilityEmbryoList.Add(emb);
        }

        // Add equip abilities
        if (Avatar.EquipOpenConfigs != null)
        {
            foreach (string ability in Avatar.EquipOpenConfigs)
            {
                AbilityEmbryo emb = new()
                {
                    AbilityId = ++embryoId,
                    AbilityNameHash = Utils.AbilityHash(ability),
                    AbilityOverrideNameHash = Utils.AbilityHash("Default")
                };

                abilityControlBlock.AbilityEmbryoList.Add(emb);
            }
        }

        return abilityControlBlock;
    }
}
