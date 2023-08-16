using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Systems.World;

namespace Weedwacker.GameServer.Systems.Ability
{
    public readonly struct AbilityInstance
    {
        public ConfigAbility Data { get; }
        public BaseEntity? Owner { get; }

        public AbilityInstance(ConfigAbility ability, BaseEntity? owner)
        {
            Data = ability;
            Owner = owner;
        }
    }
}
