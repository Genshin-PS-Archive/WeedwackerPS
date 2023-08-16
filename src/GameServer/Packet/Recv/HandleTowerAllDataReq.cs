using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.TowerAllDataReq)]
	private async Task HandleTowerAllDataReq(byte[] header, byte[] payload)
	{
		//TODO
		await SendPacketAsync(new PacketTowerAllDataRsp());
	}
}
