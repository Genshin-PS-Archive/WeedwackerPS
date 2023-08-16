using Google.Protobuf;
using Weedwacker.GameServer.Enums;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketAvatarPromoteRsp : BasePacket
    {
        public PacketAvatarPromoteRsp(ulong guid) : base(OpCode.AvatarUpgradeRsp)
        {

            AvatarPromoteRsp proto = new()
            {
                Guid = guid
            };

            Data = proto.ToByteArray();
        }
    }
}
