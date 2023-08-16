using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.PlayerLoginReq)]
	private async Task HandlePlayerLoginReq(byte[] header, byte[] payload)
	{
		// Check
		if (Player == null)
		{
			Stop();
			return;
		}

		// Parse request
		PlayerLoginReq req = PlayerLoginReq.Parser.ParseFrom(payload);

		// Authenticate session
		if (req.Token != Player.Token)
		{
			Stop();
			return;
		}

		// Login Setup
		await Player.OnLoginAsync();

		// Final packet to tell client logging in is done
		await SendPacketAsync(new PacketPlayerLoginRsp(this));
	}
}
