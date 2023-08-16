using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;
using static Weedwacker.Shared.Network.Proto.MarkMapReq.Types;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.MarkMapReq)]
	private async Task HandleMarkMapReq(byte[] header, byte[] payload)
	{
		MarkMapReq req = MarkMapReq.Parser.ParseFrom(payload);
		Operation op = req.Op;
		switch (op)
		{
			case Operation.Add:
				if (req.Mark.PointType == MapMarkPointType.FishPool)
				{
					await Player.MapMarksManager.Teleport(req);
				}
				break;
			case Operation.Mod:
				//TODO:
				break;
			case Operation.Del:
				//TODO:
				break;
			case Operation.Get:
				//TODO
				break;
		}
		await SendPacketAsync(new PacketMarkMapRsp());
	}
}
