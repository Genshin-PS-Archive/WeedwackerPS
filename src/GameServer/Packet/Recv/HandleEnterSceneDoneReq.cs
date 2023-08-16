using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.EnterSceneDoneReq)]
	private async Task HandleEnterSceneDoneReq(byte[] header, byte[] payload)
	{
		// Finished loading
		Player.SceneLoadState = SceneLoadState.LOADED;

		// Done

		// Spawn player in world
		await Player.Scene.SpawnPlayerAsync(Player);

		// Spawn other entites already in world
		await Player.Scene.ShowOtherEntitiesAsync(Player);

		// Locations
		await SendPacketAsync(new PacketWorldPlayerLocationNotify(Player.World));
		await SendPacketAsync(new PacketScenePlayerLocationNotify(Player.Scene));
		await SendPacketAsync(new PacketWorldPlayerRTTNotify(Player.World));

		//Rsp
		await SendPacketAsync(new PacketEnterSceneDoneRsp(Player));
	}
}
