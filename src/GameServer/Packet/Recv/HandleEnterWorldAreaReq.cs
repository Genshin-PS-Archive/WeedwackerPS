using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.EnterWorldAreaReq)]
	private async Task HandleEnterWorldAreaReq(byte[] header, byte[] payload)
	{
		/*
		 * The client determines the AreaID from its position relative to
		 * the polygonData's min and max Area in sceneX_worldArea.json
		 * AreaType == 1 => looks in level1Area, use id2
		 * AreaType == 2 => looks in level2Areas, use id1
		 */
		EnterWorldAreaReq req = EnterWorldAreaReq.Parser.ParseFrom(payload);
		uint areaId = req.AreaId;
		uint areaType = req.AreaType;

		await Player.EnterWorldAreaAsync(areaType, areaId);

		await SendPacketAsync(new PacketEnterWorldAreaRsp(Player, areaId, areaType));
	}
}
