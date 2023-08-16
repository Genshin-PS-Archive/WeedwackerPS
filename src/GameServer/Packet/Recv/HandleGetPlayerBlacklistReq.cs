using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetPlayerBlacklistReq)]
	private async Task HandleGetPlayerBlacklistReq(byte[] header, byte[] payload)
	{
		await SendPacketAsync(new PacketGetPlayerBlacklistRsp(Player));
	}
}
