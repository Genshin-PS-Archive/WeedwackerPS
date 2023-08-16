﻿using Google.Protobuf;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketLifeStateChangeNotify : BasePacket
    {
        public PacketLifeStateChangeNotify(SceneEntity entity, LifeState lifeState, uint sourceEntityId = 0,
                                           string attackTag = "", Shared.Network.Proto.PlayerDieType dieType = Shared.Network.Proto.PlayerDieType.None) : base(OpCode.LifeStateChangeNotify)
        {
            LifeStateChangeNotify proto = new LifeStateChangeNotify()
            {
                EntityId = entity.EntityId,
                LifeState = (uint)lifeState,
                SourceEntityId = sourceEntityId,
                AttackTag = attackTag,
                DieType = dieType,
                //MoveReliableSeq =
            };
            //proto.ServerBuffList.Add();

            Data = proto.ToByteArray();
        }
    }
}
