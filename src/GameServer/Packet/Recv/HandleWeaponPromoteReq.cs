using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.GameServer.Systems.Inventory;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.WeaponPromoteReq)]
	private async Task HandleWeaponPromoteReq(byte[] header, byte[] payload)
	{
		WeaponPromoteReq req = WeaponPromoteReq.Parser.ParseFrom(payload);
		uint oldPromote = (Player.Inventory.GuidMap[req.TargetWeaponGuid] as WeaponItem).PromoteLevel;
		WeaponItem weapon = await Player.Inventory.PromoteWeaponAsync(req.TargetWeaponGuid);
		await Player.SendPacketAsync(new PacketWeaponPromoteRsp(weapon, oldPromote));
	}
}

