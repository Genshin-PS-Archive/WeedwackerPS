using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.GadgetInteractReq)]
	private async Task HandleGadgetInteractReq(byte[] header, byte[] payload)
	{
		GadgetInteractReq req = GadgetInteractReq.Parser.ParseFrom(payload);
		BaseGadgetEntity? gadget = Player.Scene.GetEntityById(req.GadgetEntityId) as BaseGadgetEntity;
		if (gadget is null)
			return;
		await gadget.OnInteractAsync(Player, req);
		await Player.Scene?.BroadcastPacketAsync(new PacketGadgetInteractRsp(req.GadgetId, req.GadgetEntityId, gadget.InteractType, null));
	}
}
