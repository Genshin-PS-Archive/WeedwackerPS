using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetShopReq)]
	private async Task HandleGetShopReq(byte[] header, byte[] payload)
	{
		GetShopReq req = GetShopReq.Parser.ParseFrom(payload);
		await SendPacketAsync(new PacketGetShopRsp(Player, req.ShopType));
	}
}
