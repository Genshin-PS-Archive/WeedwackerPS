using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetPlayerSocialDetailReq)]
	private async Task HandleGetPlayerSocialDetailReq(byte[] header, byte[] payload)
	{
		GetPlayerSocialDetailReq req = GetPlayerSocialDetailReq.Parser.ParseFrom(payload);

		SocialDetail? detail = await GameServer.GetSocialDetailByUid(Player.GameUid, req.Uid);
		await SendPacketAsync(new PacketGetPlayerSocialDetailRsp(detail));
	}
}
