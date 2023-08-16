﻿using Google.Protobuf;
using Weedwacker.GameServer.Systems.Inventory;
using Weedwacker.GameServer.Systems.Player;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketPlayerEnterSceneInfoNotify : BasePacket
    {
        public PacketPlayerEnterSceneInfoNotify(Player player) : base(Enums.OpCode.PlayerEnterSceneInfoNotify)
        {
            PlayerEnterSceneInfoNotify proto = new()
            {
                CurAvatarEntityId = player.TeamManager.CurrentAvatarEntity.EntityId,
                EnterSceneToken = player.EnterSceneToken
            };

            proto.TeamEnterInfo = new TeamEnterSceneInfo()
            {
                TeamEntityId = player.TeamManager.EntityId,
                TeamAbilityInfo = new AbilitySyncStateInfo(), //player.TeamManager.AbilitySyncState,
                AbilityControlBlock = new AbilityControlBlock() //TODO
            };

            proto.MpLevelEntityInfo = new MPLevelEntityInfo()
            {
                AuthorityPeerId = player.World.HostPeerId,
                AbilityInfo = new AbilitySyncStateInfo(), //TODO
                EntityId = player.World.LevelEntityId
            };

            foreach (AvatarEntity avatar in player.TeamManager.ActiveTeam.Values)
            {
                WeaponItem weapon = avatar.Avatar.Weapon;

                AvatarEnterSceneInfo avatarInfo = new()
                {
                    AvatarGuid = avatar.Avatar.Guid,
                    AvatarEntityId = avatar.EntityId,
                    WeaponGuid = weapon.Guid,
                    WeaponEntityId = weapon.WeaponEntityId,
                    AvatarAbilityInfo = new AbilitySyncStateInfo(), //TODO
                    WeaponAbilityInfo = new AbilitySyncStateInfo() //TODO
                };

                proto.AvatarEnterInfo.Add(avatarInfo);
            }

            Data = proto.ToByteArray();
        }
    }
}
