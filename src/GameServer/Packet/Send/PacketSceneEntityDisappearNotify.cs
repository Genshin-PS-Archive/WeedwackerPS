using Google.Protobuf;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Packet.Send
{
    internal class PacketSceneEntityDisappearNotify : BasePacket
    {
        public PacketSceneEntityDisappearNotify(SceneEntity entity, VisionType disappearType) : base(Enums.OpCode.SceneEntityDisappearNotify)
        {
            SceneEntityDisappearNotify proto = new()
            {
                DisappearType = disappearType,
                EntityList = { entity.EntityId }
            };

            Data = proto.ToByteArray();
        }

        public PacketSceneEntityDisappearNotify(IEnumerable<SceneEntity> entities, VisionType disappearType) : base(Enums.OpCode.SceneEntityDisappearNotify)
        {
            SceneEntityDisappearNotify proto = new()
            {
                DisappearType = disappearType,
                EntityList = { entities.Select(x => x.EntityId) }
            };

            Data = proto.ToByteArray();
        }
    }
}
