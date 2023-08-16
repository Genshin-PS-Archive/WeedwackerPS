using System.Numerics;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.BinOut.Scene.Point;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;
using EnterReason = Weedwacker.GameServer.Enums.EnterReason;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.PersonalSceneJumpReq)]
	private async Task HandlePersonalSceneJumpReq(byte[] header, byte[] payload)
	{
		PersonalSceneJumpReq req = PersonalSceneJumpReq.Parser.ParseFrom(payload);

		Retcode ret = Retcode.RetFail;
		Vector3 rot = new();
		Vector3 pos = new();
		uint transToSceneId = 0;
		if (GameData.ScenePointDataMap.TryGetValue($"scene{Player.SceneId}_point", out ScenePointData? spd))
		{
			if (spd.points!.TryGetValue(req.PointId.ToString(), out ConfigScenePoint? bp))
			{
				ret = Retcode.RetSucc;
				transToSceneId = (bp as PersonalSceneJumpPoint).tranSceneId;
				if (transToSceneId is 0)
					transToSceneId = Player.Scene.PrevScene;

				pos = new Vector3(
					bp.tranPos.x,
					bp.tranPos.y,
					bp.tranPos.z);
				rot = new Vector3();
				if (bp.tranRot is not null)
				{
					rot = new Vector3(
						bp.tranRot.x,
						bp.tranRot.y,
						bp.tranRot.z);
				}
				await Player.World.TransferPlayerToSceneAsync(Player, EnterReason.PersonalScene, EnterType.Jump, transToSceneId, pos, rot, false);

			}
			else
				Logger.WriteErrorLine($"Scene {Player.SceneId} | Point  {req.PointId} not found!");
		}
		else
			Logger.WriteErrorLine($"Scene {Player.SceneId} not found!");

		await SendPacketAsync(new PacketPersonalSceneJumpRsp(ret, (uint)transToSceneId, pos));
	}
}
