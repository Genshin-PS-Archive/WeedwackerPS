using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.EntityAiSyncNotify)]
	private async Task HandleEntityAiSyncNotify(byte[] header, byte[] payload)
	{
		EntityAiSyncNotify req = EntityAiSyncNotify.Parser.ParseFrom(payload);

		if (req.LocalAvatarAlertedMonsterList.Count > 0)
		{
			await Player.Scene.BroadcastPacketAsync(new PacketEntityAiSyncNotify(req));
		}
	}
}
