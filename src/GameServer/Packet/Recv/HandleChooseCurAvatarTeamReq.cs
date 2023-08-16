using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.ChooseCurAvatarTeamReq)]
	private async Task HandleChooseCurAvatarTeamReq(byte[] header, byte[] payload)
	{
		ChooseCurAvatarTeamReq p = ChooseCurAvatarTeamReq.Parser.ParseFrom(payload);

		await Player.TeamManager.SetCurrentTeam(p.TeamId);

		await Player.SendPacketAsync(new PacketChooseCurAvatarTeamRsp(p.TeamId));
	}
}
