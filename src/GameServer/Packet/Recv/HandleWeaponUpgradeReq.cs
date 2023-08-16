using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.GameServer.Systems.Inventory;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.WeaponUpgradeReq)]
	private async Task HandleWeaponUpgradeReq(byte[] header, byte[] payload)
	{
		WeaponUpgradeReq req = WeaponUpgradeReq.Parser.ParseFrom(payload);
		List<ItemParam> leftoverOres = new(); //TODO
		uint oldLevel = (Player.Inventory.GuidMap[req.TargetWeaponGuid] as WeaponItem).Level;
		WeaponItem weapon = await Player.Inventory.UpgradeWeaponAsync(req.TargetWeaponGuid, req.FoodWeaponGuidList.ToList(), req.ItemParamList.ToList());
		await Player.SendPacketAsync(new PacketWeaponUpgradeRsp(weapon, oldLevel, leftoverOres));
	}
}

