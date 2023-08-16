using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.SetOpenStateReq)]
	private async Task HandleSetOpenStateReq(byte[] header, byte[] payload)
	{
		SetOpenStateReq? req = SetOpenStateReq.Parser.ParseFrom(payload);
		uint openState = req.Key;
		uint value = req.Value;

		if (await Player.OpenStateManager.SetOpenStateFromClientAsync(openState, value))
			await SendPacketAsync(new PacketSetOpenStateRsp(openState, value));
		else
			await SendPacketAsync(new PacketSetOpenStateRsp(Retcode.RetFail));
	}
}
