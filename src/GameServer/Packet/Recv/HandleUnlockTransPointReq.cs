using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.UnlockTransPointReq)]
	private async Task HandleUnlockTransPointReq(byte[] header, byte[] payload)
	{
		UnlockTransPointReq req = UnlockTransPointReq.Parser.ParseFrom(payload);
		bool unlocked = await Player.ProgressManager.UnlockTransPoint((int)req.SceneId, (int)req.PointId);
		await SendPacketAsync(new PacketUnlockTransPointRsp(unlocked ? Retcode.RetSucc : Retcode.RetFail));
	}
}
