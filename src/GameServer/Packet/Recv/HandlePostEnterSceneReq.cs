using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.PostEnterSceneReq)]
	private async Task HandlePostEnterSceneReq(byte[] header, byte[] payload)
	{
		if (Player.Scene.SceneData.type == SceneType.SCENE_ROOM)
		{
			//await session.Player.QuestManager.TriggerEventAsync(QuestTrigger.QUEST_CONTENT_ENTER_ROOM, session.Player.SceneId, 0);
		}

		await SendPacketAsync(new PacketPostEnterSceneRsp(Player));
	}
}
