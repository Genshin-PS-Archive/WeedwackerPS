using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.EvtCreateGadgetNotify)]
	private async Task HandleEvtCreateGadgetNotify(byte[] header, byte[] payload)
	{
		EvtCreateGadgetNotify req = EvtCreateGadgetNotify.Parser.ParseFrom(payload);

		// Sanity check - dont add duplicate entities
		if (Player.Scene.GetEntityById(req.EntityId) != null)
		{
			return;
		}

		// Create entity and summon in world
		ClientGadgetEntity gadget = new ClientGadgetEntity(Player.Scene, Player, req);

		await Player.Scene.OnPlayerCreateGadget(gadget);
	}
}
