using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetPlayerFriendListReq)]
	private async Task HandleGetPlayerFriendListReq(byte[] header, byte[] payload)
	{
		await SendPacketAsync(new PacketGetPlayerFriendListRsp(Player));
	}
}
