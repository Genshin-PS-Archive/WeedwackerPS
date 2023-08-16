using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetWidgetSlotReq)]
	private async Task HandleGetWidgetSlotReq(byte[] header, byte[] payload)
	{
		await SendPacketAsync(new PacketGetWidgetSlotRsp(Player));
	}
}
