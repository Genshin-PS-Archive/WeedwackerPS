using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.SetUpAvatarTeamReq)]
	private async Task HandleSetUpAvatarTeamReq(byte[] header, byte[] payload)
	{
		SetUpAvatarTeamReq req = SetUpAvatarTeamReq.Parser.ParseFrom(payload);

		await Player.TeamManager.SetupAvatarTeamAsync((int)req.TeamId, req.AvatarTeamGuidList);
		await Player.SendPacketAsync(new PacketSetUpAvatarTeamRsp(Player, req.TeamId, req.AvatarTeamGuidList));
	}
}
