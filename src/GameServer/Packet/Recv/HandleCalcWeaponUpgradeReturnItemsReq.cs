using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.CalcWeaponUpgradeReturnItemsReq)]
	private async Task HandleCalcWeaponUpgradeReturnItemsReq(byte[] header, byte[] payload)
	{
		CalcWeaponUpgradeReturnItemsReq req = CalcWeaponUpgradeReturnItemsReq.Parser.ParseFrom(payload);
		List<ItemParam> returnOres = new(); //TODO
		await SendPacketAsync(new PacketCalcWeaponUpgradeReturnItemsRsp(req.TargetWeaponGuid, returnOres));
	}
}

