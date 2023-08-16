using Google.Protobuf;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketOpenStateChangeNotify : BasePacket
    {
        public PacketOpenStateChangeNotify(uint openState, int value) : base(Enums.OpCode.OpenStateChangeNotify)
        {
            OpenStateChangeNotify proto = new();
            proto.OpenStateMap.Add(openState, (uint)value);

            Data = proto.ToByteArray();
        }

        public PacketOpenStateChangeNotify(IEnumerable<Tuple<uint, int>> openStates) : base(Enums.OpCode.OpenStateChangeNotify)
        {

            OpenStateChangeNotify proto = new();
            foreach (Tuple<uint, int>? openState in openStates)
            {
                proto.OpenStateMap.Add(openState.Item1, (uint)openState.Item2);
            }

            Data = proto.ToByteArray();
        }
    }
}
