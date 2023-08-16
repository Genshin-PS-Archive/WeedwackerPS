using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.PlayerSetPauseReq)]
	private async Task HandlePlayerSetPauseReq(byte[] header, byte[] payload)
	{
		PacketHead head = PacketHead.Parser.ParseFrom(header);
		PlayerSetPauseReq req = PlayerSetPauseReq.Parser.ParseFrom(payload);

		await SendPacketAsync(new PacketPlayerSetPauseRsp(head.ClientSequenceId));
		Player.SetPaused(req.IsPaused);
	}
}
