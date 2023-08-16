using Google.Protobuf;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Player;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketOpenStateUpdateNotify : BasePacket
    {
        public PacketOpenStateUpdateNotify(Player player) : base(Enums.OpCode.OpenStateUpdateNotify)
        {
            OpenStateUpdateNotify proto = new();

            foreach (KeyValuePair<OpenStateType, int> openState in player.ProgressManager.OpenStates)
            {
                proto.OpenStateMap.Add((uint)openState.Key, (uint)openState.Value);
            }

            Data = proto.ToByteArray();
        }
    }
}
