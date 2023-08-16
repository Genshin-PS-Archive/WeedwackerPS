using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.AvatarPromoteReq)]
	private async Task HandleAvatarPromoteReq(byte[] header, byte[] payload)
	{
		AvatarPromoteReq req = AvatarPromoteReq.Parser.ParseFrom(payload);
		await Player.Avatars.PromoteAvatarAsync(req.Guid);
		await Player.SendPacketAsync(new PacketAvatarPromoteRsp(req.Guid));
	}
}
