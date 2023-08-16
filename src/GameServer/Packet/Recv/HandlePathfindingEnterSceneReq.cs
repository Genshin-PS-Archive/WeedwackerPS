using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.PathfindingEnterSceneReq)]
	private async Task HandlePathfindingEnterSceneReq(byte[] header, byte[] payload)
	{
		PacketHead head = PacketHead.Parser.ParseFrom(header);
		PathfindingEnterSceneReq req = PathfindingEnterSceneReq.Parser.ParseFrom(payload);
		await SendPacketAsync(new PacketPathfindingEnterSceneRsp(head.ClientSequenceId));
	}
}
