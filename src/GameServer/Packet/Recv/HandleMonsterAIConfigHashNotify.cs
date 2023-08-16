using Weedwacker.GameServer.Enums;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.MonsterAIConfigHashNotify)]
	private async Task HandleMonsterAIConfigHashNotify(byte[] header, byte[] payload)
	{
		// TODO: Handle MonsterAIConfigHashNotify
	}
}
