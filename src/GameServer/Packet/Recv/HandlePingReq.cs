using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.PingReq)]
	private async Task HandlePingReq(byte[] header, byte[] payload)
	{
		PacketHead head = PacketHead.Parser.ParseFrom(header);
		PingReq ping = PingReq.Parser.ParseFrom(payload);

		UpdateLastPingTime(ping.ClientTime);

		await SendPacketAsync(new PacketPingRsp(head.ClientSequenceId, ping.ClientTime));
	}
}
