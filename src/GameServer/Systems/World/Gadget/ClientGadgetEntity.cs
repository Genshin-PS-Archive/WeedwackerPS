using System.Numerics;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Systems.World
{
    internal class ClientGadgetEntity : BaseGadgetEntity
    {
        public readonly Player.Player Owner;

        public readonly uint CampId;
        public readonly uint CampType;
        public uint ConfigId => GadgetId; // I think this is wrong
        public readonly uint OwnerEntityId;
        public readonly uint TargetEntityId;
        public readonly bool AsyncLoad;
        public readonly uint OriginalOwnerEntityId;

        public ClientGadgetEntity(Scene scene, Player.Player player, EvtCreateGadgetNotify notify) : base(scene, notify.ConfigId)
        {
            Owner = player;
            EntityId = notify.EntityId;
            Position = new Vector3(notify.InitPos.X, notify.InitPos.Y, notify.InitPos.Z);
            Rotation = new Vector3(notify.InitEulerAngles.X, notify.InitEulerAngles.Y, notify.InitEulerAngles.Z);
            GadgetId = notify.ConfigId;
            CampId = notify.CampId;
            CampType = notify.CampType;
            OwnerEntityId = notify.PropOwnerEntityId;
            TargetEntityId = notify.TargetEntityId;
			//AsyncLoad = notify.IsAsyncLoad;

			if (scene.GetEntityById(OwnerEntityId) is SceneEntity owner && owner is ClientGadgetEntity ownerGadget)
			{
				OriginalOwnerEntityId = ownerGadget.OriginalOwnerEntityId;
			}
			else
			{
				OriginalOwnerEntityId = OwnerEntityId;
			}
		}

        public override SceneEntityInfo ToProto()
        {
            SceneEntityInfo? info = base.ToProto();

            ClientGadgetInfo clientGadget = new()
            {
                CampId = CampId,
                CampType = CampType,
                OwnerEntityId = OwnerEntityId,
                TargetEntityId = TargetEntityId,
                AsyncLoad = AsyncLoad
            };
            info.Gadget.OwnerEntityId = OwnerEntityId;
            info.Gadget.ClientGadget = clientGadget;
            info.Gadget.PropOwnerEntityId = OwnerEntityId;

            return info;
        }
    }
}
