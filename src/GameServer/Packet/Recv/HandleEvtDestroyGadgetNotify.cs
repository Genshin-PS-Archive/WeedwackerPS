using Weedwacker.GameServer.Enums;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.EvtDestroyGadgetNotify)]
	private async Task HandleEvtDestroyGadgetNotify(byte[] header, byte[] payload)
	{
		EvtDestroyGadgetNotify req = EvtDestroyGadgetNotify.Parser.ParseFrom(payload);

		await Player.Scene.OnPlayerDestroyGadget(req.EntityId);
	}
}
