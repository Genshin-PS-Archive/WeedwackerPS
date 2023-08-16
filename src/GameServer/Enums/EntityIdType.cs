﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Weedwacker.GameServer.Enums
{
	[JsonConverter(typeof(StringEnumConverter))]
	public enum EntityIdType
    {
        AVATAR = 0x01,
        MONSTER = 0x02,
        NPC = 0x03,
        GADGET = 0x04,
        REGION = 0x05,
        WEAPON = 0x06,
        TEAM = 0x09,
        MPLEVEL = 0x0b
    }
}
