using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.ChangeGameTimeReq)]
	private async Task HandleChangeGameTimeReq(byte[] header, byte[] payload)
	{
		ChangeGameTimeReq req = ChangeGameTimeReq.Parser.ParseFrom(payload);
		Player.Scene.ChangeTime((int)req.GameTime);
		SendPacketAsync(new PacketChangeGameTimeRsp(Player));
	}
}
