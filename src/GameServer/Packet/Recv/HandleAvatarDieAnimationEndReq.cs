using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.AvatarDieAnimationEndReq)]
	private async Task HandleAvatarDieAnimationEndReq(byte[] header, byte[] payload)
	{
		AvatarDieAnimationEndReq req = AvatarDieAnimationEndReq.Parser.ParseFrom(payload);

		await Player.TeamManager.OnAvatarDie((long)req.DieGuid);

		await SendPacketAsync(new PacketAvatarDieAnimationEndRsp(req.DieGuid, 0));
	}
}
