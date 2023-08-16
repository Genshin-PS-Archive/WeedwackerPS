using Weedwacker.GameServer.Systems.Script.Scene;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Systems.World
{
    internal class PlatformEntity : ScriptGadgetEntity
    {
        private bool IsActive;
        private bool IsStarted;
        private uint RouteId;
        private uint AreaId;
        private MovingPlatformType MovingType;

        internal PlatformEntity(Scene? scene, SceneGroup.Gadget spawnInfo) : base(scene, spawnInfo)
        {
            RouteId = spawnInfo.route_id;
            AreaId = spawnInfo.area_id;
            IsActive = true;
            IsStarted = false;
        }

        public override SceneEntityInfo ToProto()
        {
            SceneEntityInfo? info = base.ToProto();
            PlatformInfo platform = new()
            {
                IsActive = IsActive,
                IsStarted = IsStarted,
                //PointId = ,
                //MovingPlatformType = MovingType,
                //PosOffset = ,
                //StartPos =,
                //StartIndex =,
                //StartRot =,
                //StartRouteTime =,
                //StartSceneTime =,
                //StopSceneTime =,
                //RotOffset = ,
                //Route =,
                //RouteId =,
            };

            info.Gadget.Platform = platform;
            return info;
        }
    }
}
