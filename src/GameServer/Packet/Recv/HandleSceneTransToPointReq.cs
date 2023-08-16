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
	[OpCode((ushort)OpCode.SceneTransToPointReq)]
	private async Task HandleSceneTransToPointReq(byte[] header, byte[] payload)
	{
		SceneTransToPointReq req = SceneTransToPointReq.Parser.ParseFrom(payload);

		Retcode ret = Retcode.RetFail;
		if (GameData.ScenePointDataMap.TryGetValue($"scene{req.SceneId}_point", out ScenePointData? spd))
		{
			if (spd.points!.TryGetValue(req.PointId.ToString(), out ConfigScenePoint? bp))
			{
				ret = Retcode.RetSucc;
				Vector3 pos = new(
					bp.tranPos.x,
					bp.tranPos.y,
					bp.tranPos.z);
				Vector3 rot = new();
				if (bp.tranRot is not null)
				{
					rot = new Vector3(
						bp.tranRot.x,
						bp.tranRot.y,
						bp.tranRot.z);
				}
				await Player.World.TransferPlayerToSceneAsync(Player, EnterReason.TransPoint,
					req.SceneId == Player.SceneId ? EnterType.Goto : EnterType.Jump, req.SceneId,
					pos, rot, false);
			}
			else
				Logger.WriteErrorLine($"Scene {req.SceneId} | Point  {req.PointId} not found!");
		}
		else
			Logger.WriteErrorLine($"Scene {req.SceneId} not found!");

		await SendPacketAsync(new PacketSceneTransToPointRsp(ret, req.SceneId, req.PointId));
	}
}
