using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Player
{
    internal class EnergyManager
    {
        private Player Owner;

        public EnergyManager(Player player)
        {
            Owner = player;
        }

        internal void HandleMonsterEnergyDrop(MonsterEntity monsterEntity, float hpBeforeDamage, float hpAfterDamage)
        {
            Logger.DebugWriteLine("MonsterEnergyDrop not implemented.");
        }
    }
}