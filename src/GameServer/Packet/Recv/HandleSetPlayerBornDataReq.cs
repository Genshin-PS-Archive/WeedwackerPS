using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Packet;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer;
internal partial class Connection
{
	[OpCode((ushort)OpCode.SetPlayerBornDataReq)]
	private async Task HandleSetPlayerBornDataReq(byte[] header, byte[] payload)
	{
		SetPlayerBornDataReq proto = SetPlayerBornDataReq.Parser.ParseFrom(payload);
		string heroName = proto.NickName;
		uint avatarId = proto.AvatarId;

		if (GameData.AvatarHeroEntityDataMap.ContainsKey(avatarId))
		{
			Avatar? newAvatar = await Avatar.CreateAsync(avatarId, Player);
			await Player.SetMainCharacter(avatarId, heroName);
			await Player.Avatars.AddAvatar(newAvatar, false);
			await Player.OnLoginAsync();
			await SendPacketAsync(new BasePacket(OpCode.SetPlayerBornDataRsp));
		}
		else
		{
			//TODO punish cheaters
		}
	}
}
