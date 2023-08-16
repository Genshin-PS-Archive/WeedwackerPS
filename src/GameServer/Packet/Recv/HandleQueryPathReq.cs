using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.QueryPathReq)]
	private async Task HandleQueryPathReq(byte[] header, byte[] payload)
	{
		var req = QueryPathReq.Parser.ParseFrom(payload);

		/**
		 * It is not the actual work
		 */

		if (req.DestinationPos.Count > 0)
		{
			await SendPacketAsync(new PacketQueryPathRsp(req));
		}
	}
}
