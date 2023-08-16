using Google.Protobuf;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Player;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send;

internal class PacketPlayerPropNotify : BasePacket
{
	public PacketPlayerPropNotify(Player player, PropType prop) : base(OpCode.PlayerPropNotify)
	{
		BuildHeader(0);

		PlayerPropNotify proto = new();
		proto.PropMap.Add((uint)prop, new PropValue() { Type = (uint)prop, Ival = player.PlayerProperties[prop], Val = player.PlayerProperties[prop] });

		Data = proto.ToByteArray();
	}
}
