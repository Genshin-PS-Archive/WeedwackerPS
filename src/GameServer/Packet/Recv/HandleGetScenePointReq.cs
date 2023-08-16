using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetScenePointReq)]
	private async Task HandleGetScenePointReq(byte[] header, byte[] payload)
	{
		GetScenePointReq req = GetScenePointReq.Parser.ParseFrom(payload);
		await SendPacketAsync(new PacketGetScenePointRsp(Player, req.SceneId));
	}
}
