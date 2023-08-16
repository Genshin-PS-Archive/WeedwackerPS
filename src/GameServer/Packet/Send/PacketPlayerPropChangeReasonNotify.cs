using Google.Protobuf;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send;

internal class PacketPlayerPropChangeReasonNotify : BasePacket
{
	public PacketPlayerPropChangeReasonNotify(PropType prop, int oldValue, int curValue, PropChangeReason reason) : base(OpCode.PlayerPropChangeReasonNotify)
	{
		BuildHeader(0);

		PlayerPropChangeReasonNotify proto = new PlayerPropChangeReasonNotify()
		{
			PropType = (uint)prop,
			Reason = reason,
			OldValue = (uint)oldValue,
			CurValue = (uint)curValue
		};

		Data = proto.ToByteArray();
	}
}
