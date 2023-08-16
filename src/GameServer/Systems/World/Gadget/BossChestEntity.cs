using Weedwacker.GameServer.Systems.Script.Scene;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Systems.World
{
    internal class BossChestEntity : ScriptGadgetEntity
    {
        private readonly uint MonsterConfigId;
        private readonly uint Resin;
        internal BossChestEntity(Scene? scene, SceneGroup.Gadget spawnInfo) : base(scene, spawnInfo)
        {
            MonsterConfigId = spawnInfo.boss_chest.monster_config_id;
            Resin = spawnInfo.boss_chest.resin;
        }

        public override SceneEntityInfo ToProto()
        {
            SceneEntityInfo? info = base.ToProto();

            BossChestInfo bossChest = new()
            {
                MonsterConfigId = MonsterConfigId,
                Resin = Resin,
            };
            //bossChest.QualifyUidList.AddRange();
            //bossChest.RemainUidList.AddRange();
            //bossChest.UidDiscountMap.Add();

            info.Gadget.BossChest = bossChest;

            return info;
        }
    }
}
