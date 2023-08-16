using Weedwacker.GameServer.Enums;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.EvtDoSkillSuccNotify)]
	private async Task HandleEvtDoSkillSuccNotify(byte[] header, byte[] payload)
	{
		//TODO
	}
}
