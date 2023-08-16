using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.SceneInitFinishReq)]
	private async Task HandleSceneInitFinishReq(byte[] header, byte[] payload)
	{
		// Info packets
		await Task.WhenAll(new Task[]
		{
			SendPacketAsync(new PacketServerTimeNotify()),
			Player.World.BroadcastPacketAsync(new PacketWorldPlayerInfoNotify(Player.World)),
			SendPacketAsync(new PacketWorldDataNotify(Player.World)),
			SendPacketAsync(new PacketPlayerWorldSceneInfoListNotify(Player.Scene)),
			SendPacketAsync(new PacketSceneForceUnlockNotify()),
			SendPacketAsync(new PacketTeamResonanceChangeNotify(Player.TeamManager.CurrentTeamInfo)),
			SendPacketAsync(new BasePacket(OpCode.SceneDataNotify)),
			SendPacketAsync(new PacketHostPlayerNotify(Player.World)),
			SendPacketAsync(new PacketSceneTimeNotify(Player)),
			SendPacketAsync(new PacketPlayerGameTimeNotify(Player)),
			Player.Scene.BroadcastPacketAsync(new PacketPlayerEnterSceneInfoNotify(Player)),
			Player.Scene.UpdateActiveAreaWeathersAsync(Player.WorldAreaIds),

			SendPacketAsync(new PacketScenePlayerInfoNotify(Player.World)),
			Player.Scene.BroadcastPacketAsync(new PacketSceneTeamUpdateNotify(Player)),
			Player.Scene.BroadcastPacketAsync(new PacketSyncTeamEntityNotify(Player)),
			Player.Scene.BroadcastPacketAsync(new PacketSyncScenePlayTeamEntityNotify(Player)),
		});

		// Done Packet
		await SendPacketAsync(new PacketSceneInitFinishRsp(Player));

		// Set state
		Player.SceneLoadState = SceneLoadState.INIT;
	}
}
