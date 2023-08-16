using Google.Protobuf;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.GameServer.Systems.Inventory;
using Weedwacker.GameServer.Systems.Player;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketAvatarDataNotify : BasePacket
    {
        public PacketAvatarDataNotify(Player player) : base(Enums.OpCode.AvatarDataNotify, true)
        {
            AvatarDataNotify proto = new()
            {
                CurAvatarTeamId = (uint)player.TeamManager.CurrentTeamIndex,
            };
            foreach (uint cloak in (player.Inventory.SubInventories[ItemType.ITEM_MATERIAL] as MaterialSubInv).FlyCloakList) proto.OwnedFlycloakList.Add(cloak);
            foreach (uint costume in (player.Inventory.SubInventories[ItemType.ITEM_MATERIAL] as MaterialSubInv).CostumeList) proto.OwnedCostumeList.Add(costume);

            foreach (Avatar avatar in player.Avatars.Avatars.Values) proto.AvatarList.Add(avatar.ToProto());

            foreach (KeyValuePair<int, TeamInfo> entry in player.TeamManager.Teams)
            {
                // check if not all slots are empty
                if (entry.Value.AvatarInfo.Keys.Distinct().Count() >= 1)
                {
                    TeamInfo teamInfo = entry.Value;
                    proto.AvatarTeamMap.Add((uint)entry.Key, teamInfo.ToProto(player));
                }
                else
                    proto.AvatarTeamMap.Add((uint)entry.Key, new AvatarTeam());
            }

            // Set main character
            Avatar? mainCharacter = player.Avatars.GetAvatarById(player.MainCharacterId);
            if (mainCharacter != null)
            {
                proto.ChooseAvatarGuid = mainCharacter.Guid;
            }

            Data = proto.ToByteArray();
        }
    }
}
