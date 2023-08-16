using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.AvatarUpgradeReq)]
	private async Task HandleAvatarUpgradeReq(byte[] header, byte[] payload)
	{
		AvatarUpgradeReq req = AvatarUpgradeReq.Parser.ParseFrom(payload);
		Avatar oldAvatar = Player.Avatars.AvatarsGuid[req.AvatarGuid];
		uint oldLevel = oldAvatar.Level;
		Dictionary<FightPropType, float> oldPropMap = new(oldAvatar.FightProp);
		Avatar newAvatar = await Player.Avatars.UpgradeAvatarAsync(req.AvatarGuid, req.ItemId, req.Count);
		await SendPacketAsync(new PacketAvatarUpgradeRsp(newAvatar, oldLevel, oldPropMap));
	}
}
