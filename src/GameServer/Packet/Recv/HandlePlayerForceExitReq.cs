using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.PlayerForceExitReq)]
	private async Task HandlePlayerForceExitReq(byte[] header, byte[] payload)
	{
		await SendPacketAsync(new BasePacket(OpCode.PlayerForceExitRsp));
		Stop();
	}
}
