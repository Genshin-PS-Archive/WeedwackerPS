﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Database;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Systems.Avatar;

// MongoDb collection: "avatars"
internal class AvatarManager
{
    [BsonId]
    [BsonElement("_id")]
    public uint OwnerId { get; private set; } // GameUid
    [BsonSerializer(typeof(UIntDictionarySerializer<Avatar>))]
    [BsonElement] public Dictionary<uint, Avatar> Avatars { get; private set; }
    [BsonIgnore] private Player.Player Owner; // Loaded by DatabaseManager
    [BsonIgnore] public Dictionary<ulong, Avatar> AvatarsGuid { get; private set; } = new();

    public AvatarManager(Player.Player owner)
    {
        OwnerId = owner.GameUid;
        Owner = owner;
        Avatars = new Dictionary<uint, Avatar>();
    }

    public async Task OnLoadAsync(Player.Player owner)
    {
        Owner = owner;
        AvatarsGuid = new Dictionary<ulong, Avatar>();
        List<Task>? tasks = new();
        foreach (Avatar avatar in Avatars.Values)
        {
            tasks.Add(avatar.OnLoadAsync(owner));
        }
        await Task.WhenAll(tasks);
        foreach (Avatar avatar in Avatars.Values)
        {
            AvatarsGuid.Add(avatar.Guid, avatar);
        }
    }
    public Avatar? GetAvatarById(uint id)
    {
        return Avatars.GetValueOrDefault(id, null);
    }

    public Avatar? GetAvatarByGuid(ulong id)
    {
        return AvatarsGuid.GetValueOrDefault(id, null);
    }

    public bool HasAvatar(uint id)
    {
        return Avatars.ContainsKey(id);
    }

    public async Task<bool> AddAvatar(Avatar avatar, bool notify = true)
    {
        if (avatar.Data == null || HasAvatar(avatar.AvatarId))
        {
            return false;
        }

        // Put into maps
        Avatars.Add(avatar.AvatarId, avatar);
        AvatarsGuid.Add(avatar.Guid, avatar);
        FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
        UpdateDefinition<AvatarManager>? update = Builders<AvatarManager>.Update.Set($"{nameof(Avatars)}.{avatar.AvatarId}", avatar);
        await DatabaseManager.UpdateAvatarsAsync(filter, update);

        bool addToTeam = false;
        if (notify) await Owner.SendPacketAsync(new PacketAvatarAddNotify(avatar, addToTeam));
        if (Owner.TeamManager.CurrentTeamInfo.AvatarInfo.Count < (Owner.IsInMultiplayer ? GameServer.Configuration.Server.GameOptions.AvatarLimits.MultiplayerTeam : GameServer.Configuration.Server.GameOptions.AvatarLimits.SinglePlayerTeam))
        {
            addToTeam = true;
            await Owner.TeamManager.AddToTeamAsync(avatar);
            if (Owner.World != null)
            {
                await Owner.TeamManager.UpdateTeamEntitiesAsync();
            }
        }

        return true; ;
    }

    public async Task<Avatar> UpgradeAvatarAsync(ulong avatarGuid, uint itemId, uint count)
    {
        Avatar avatar = AvatarsGuid[avatarGuid];
        MaterialData? matData = GameData.ItemDataMap[itemId] as MaterialData;
        if (!avatar.Data.PromoteData.TryGetValue(avatar.PromoteLevel, out AvatarPromoteData promoteData) || avatar.Level == promoteData.unlockMaxLevel)
            return null;
        uint expGain = 0;
        foreach (MaterialData.ItemUseConfig? x in matData.itemUse.Where(o => o.useOp == ItemUseOp.ITEM_USE_ADD_EXP)) expGain += uint.Parse(x.useParam[0]) * count;
        List<IdCountConfig> costItems = new()
        {
            new IdCountConfig{id=itemId, count=count},
            new IdCountConfig{id=202, count=expGain / 5}
        };
        if (await Owner.Inventory.PayPromoteCostAsync(costItems, ActionReason.AvatarUpgrade))
        {
            uint oldLevel = avatar.Level;
            uint maxLevel = promoteData.unlockMaxLevel;
            uint reqExp = GameData.AvatarLevelDataMap[oldLevel].Exp;
            uint exp = avatar.Exp;
            while (expGain > 0 && avatar.Level < maxLevel)
            {
                uint toGain = Math.Min(expGain, reqExp - exp);
                exp += toGain;
                expGain -= toGain;
                if (exp >= reqExp)
                {
                    exp = 0;
                    avatar.Level += 1;
                    reqExp = GameData.AvatarLevelDataMap[avatar.Level].Exp;
                }
            }
            avatar.Exp = exp;
            FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
            UpdateDefinition<AvatarManager>? update = Builders<AvatarManager>.Update.
                Set($"{nameof(Avatars)}.{avatar.AvatarId}.{nameof(avatar.Level)}", avatar.Level).
                Set($"{nameof(Avatars)}.{avatar.AvatarId}.{nameof(avatar.Exp)}", avatar.Exp);
            await DatabaseManager.UpdateAvatarsAsync(filter, update);
            await avatar.RecalcStatsAsync(true);

            await Owner.SendPacketAsync(new PacketAvatarPropNotify(avatar));
        }
        return avatar;
    }

    public async Task PromoteAvatarAsync(ulong avatarGuid)
    {
        Avatar avatar = AvatarsGuid[avatarGuid];
        AvatarPromoteData? currentPromoteData = avatar.Data.PromoteData[avatar.PromoteLevel];
        if (!avatar.Data.PromoteData.TryGetValue(avatar.PromoteLevel + 1, out AvatarPromoteData? promoteData) || Owner.PlayerProperties[PropType.PROP_PLAYER_LEVEL] < promoteData.requiredPlayerLevel)
            return;
        IEnumerable<IdCountConfig> costItems = promoteData.costItems.Where(o => o.id != 0).Append(new IdCountConfig{ id = 202, count = promoteData.scoinCost }); //some promotedatas in the excel seem to have landmines without an id -_-
        if (await Owner.Inventory.PayPromoteCostAsync(costItems, ActionReason.AvatarPromote))
        {
            avatar.PromoteLevel += 1;
            IEnumerable<AvatarSkillDepotData.ProudSkillOpenConfig>? inherentProudSkillOpensToAdd = avatar.Data.SkillDepotData[avatar.CurrentDepotId].inherentProudSkillOpens.Where(o => o.needAvatarPromoteLevel == avatar.PromoteLevel);
            foreach (AvatarSkillDepotData.ProudSkillOpenConfig? proudSkillOpen in inherentProudSkillOpensToAdd)
            {
                foreach (ProudSkillData? k in avatar.Data.ProudSkillData[avatar.CurrentDepotId].Values.Where(w => w.proudSkillGroupId == proudSkillOpen.proudSkillGroupId))
                {
                    avatar.CurSkillDepot.AddProudSkill(k.proudSkillId);
                }
            }

            FilterDefinition<AvatarManager>? filter = Builders<AvatarManager>.Filter.Where(w => w.OwnerId == Owner.GameUid);
            UpdateDefinition<AvatarManager>? update = Builders<AvatarManager>.Update.
                Set($"{nameof(Avatars)}.{avatar.AvatarId}.{nameof(Avatar.PromoteLevel)}", avatar.PromoteLevel).
                Set($"{nameof(Avatars)}.{avatar.AvatarId}{nameof(Avatar.SkillDepots)}.{avatar.CurrentDepotId}.{nameof(SkillDepot.InherentProudSkillIds)}", avatar.CurSkillDepot.InherentProudSkillIds);

            await DatabaseManager.UpdateAvatarsAsync(filter, update);
            await avatar.RecalcStatsAsync(true);
            await Owner.SendPacketAsync(new PacketProudSkillChangeNotify(avatar));
            await Owner.SendPacketAsync(new PacketAvatarPropNotify(avatar));
        }
    }
}
