using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetSceneAreaReq)]
	private async Task HandleGetSceneAreaReq(byte[] header, byte[] payload)
	{
		GetSceneAreaReq req = GetSceneAreaReq.Parser.ParseFrom(payload);
		await SendPacketAsync(new PacketGetSceneAreaRsp(Player, req.SceneId));
	}
}
