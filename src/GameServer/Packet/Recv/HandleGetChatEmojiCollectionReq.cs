using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GetChatEmojiCollectionReq)]
	private async Task HandleGetChatEmojiCollectionReq(byte[] header, byte[] payload)
	{
		await SendPacketAsync(new PacketGetChatEmojiCollectionRsp(Player));
	}
}
