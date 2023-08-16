using Google.Protobuf;
using Weedwacker.GameServer.Enums;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketChooseCurAvatarTeamRsp : BasePacket
    {
        public PacketChooseCurAvatarTeamRsp(uint teamId) : base(OpCode.ChooseCurAvatarTeamRsp)
        {
            ChooseCurAvatarTeamRsp r = new()
            {
                CurTeamId = teamId
            };
            Data = r.ToByteArray();
        }
    }
}
