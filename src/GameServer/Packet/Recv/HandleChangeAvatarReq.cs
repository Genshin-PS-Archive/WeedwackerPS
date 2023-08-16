using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.ChangeAvatarReq)]
	private async Task HandleChangeAvatarReq(byte[] header, byte[] payload)
	{
		ChangeAvatarReq req = ChangeAvatarReq.Parser.ParseFrom(payload);
		await Player.TeamManager.ChangeAvatar(req.Guid);
		await SendPacketAsync(new PacketChangeAvatarRsp(req.Guid));
	}
}
