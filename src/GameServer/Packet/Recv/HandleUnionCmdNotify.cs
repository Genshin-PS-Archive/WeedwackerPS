using Weedwacker.GameServer.Enums;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.UnionCmdNotify)]
	private async Task HandleUnionCmdNotify(byte[] header, byte[] payload)
	{
		UnionCmdNotify req = UnionCmdNotify.Parser.ParseFrom(payload);
		foreach (UnionCmd cmd in req.CmdList)
		{
			ushort cmdOpcode = (ushort)cmd.MessageId;
			byte[] cmdPayload = cmd.Body.ToByteArray();
#if DEBUG
			PacketDebugMode debugMode = GameServer.Configuration.Server.LogOptions.LogPackets;
			if (debugMode == PacketDebugMode.ALL)
			{
				goto Log;
			}
			else if (debugMode == PacketDebugMode.WHITELIST && GameServer.Configuration.Server.LogOptions.DebugWhitelist.Contains((OpCode)cmd.MessageId))
			{
				goto Log;
			}
			else if (debugMode == PacketDebugMode.BLACKLIST && !GameServer.Configuration.Server.LogOptions.DebugBlacklist.Contains((OpCode)cmd.MessageId))
			{
				goto Log;
			}
			else
				goto NotLog;
			Log:
			LogPacket("RECV in Union", cmdOpcode, cmdPayload);
		NotLog:
#endif
			// Handle the subPacket
			await HandlePacketAsync(cmdOpcode, Array.Empty<byte>(), cmd.Body.ToByteArray());
		}

		// Notify Peers
		await Player.AbilityInvNotifyList.NotifyAsync();
		await Player.CombatInvNotifyList.NotifyAsync();
		await Player.ClientAbilityInitFinishNotifyList.NotifyAsync();
	}
}
