﻿using Google.Protobuf;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Player;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send;

internal class PacketResinChangeNotify : BasePacket
{
	public PacketResinChangeNotify(Player player) : base(OpCode.ResinChangeNotify)
	{
		ResinChangeNotify proto = new()
		{
			CurValue = (uint)player.PlayerProperties[PropType.PROP_PLAYER_RESIN],
			NextAddTimestamp = (uint)player.NextResinRefresh
		};

		Data = proto.ToByteArray();
	}
}
