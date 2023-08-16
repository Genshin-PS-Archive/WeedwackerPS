using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.EnterSceneReadyReq)]
	private async Task HandleEnterSceneReadyReq(byte[] header, byte[] payload)
	{
		EnterSceneReadyReq proto = EnterSceneReadyReq.Parser.ParseFrom(payload);
		// Reject player if invalid token
		if (Player.EnterSceneToken == proto.EnterSceneToken)
		{
			await Player.World.BroadcastPacketAsync(new PacketEnterScenePeerNotify(Player));

		}
		await SendPacketAsync(new PacketEnterSceneReadyRsp(Player, proto.EnterSceneToken));
	}
}
