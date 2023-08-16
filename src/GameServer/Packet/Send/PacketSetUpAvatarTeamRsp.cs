using Google.Protobuf;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.GameServer.Systems.Player;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketSetUpAvatarTeamRsp : BasePacket
    {
        public PacketSetUpAvatarTeamRsp(Player player, uint teamId, IList<ulong> guidList) : base(OpCode.SetUpAvatarTeamRsp)
        {
            SetUpAvatarTeamRsp p = new()
            {
                TeamId = teamId,
                CurAvatarGuid = player.TeamManager.CurrentCharacterGuid,
            };
            foreach (ulong g in guidList)
            {
                p.AvatarTeamGuidList.Add(g);
            }
            

            Data = p.ToByteArray();

        }

        public PacketSetUpAvatarTeamRsp(Player player, uint teamId, TeamInfo team) : base(OpCode.SetUpAvatarTeamRsp)
        {
            SetUpAvatarTeamRsp p = new()
            {
                TeamId = teamId,
                CurAvatarGuid = player.TeamManager.CurrentCharacterGuid,
            };
            foreach (Avatar? a in team.AvatarInfo.Values)
            {
                p.AvatarTeamGuidList.Add(a.Guid);
            }


            Data = p.ToByteArray();

        }
    }
}
