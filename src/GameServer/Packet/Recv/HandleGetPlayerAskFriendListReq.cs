using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetPlayerAskFriendListReq)]
	private async Task HandleGetPlayerAskFriendListReq(byte[] header, byte[] payload)
	{
		//TODO
		await SendPacketAsync(new PacketGetPlayerAskFriendListRsp(Player));
	}
}
