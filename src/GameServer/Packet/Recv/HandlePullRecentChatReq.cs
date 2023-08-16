using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.PullRecentChatReq)]
	private async Task HandlePullRecentChatReq(byte[] header, byte[] payload)
	{
		//TODO
		await SendPacketAsync(new PacketPullRecentChatRsp());
	}
}
