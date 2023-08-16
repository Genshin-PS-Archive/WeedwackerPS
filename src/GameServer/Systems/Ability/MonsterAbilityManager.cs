using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Systems.Ability
{
    internal class MonsterAbilityManager : BaseAbilityManager
    {
        public override Dictionary<uint, Dictionary<uint, object>?>? AbilitySpecials => throw new NotImplementedException();

        public override Dictionary<string, HashSet<string>> UnlockedTalentParams => throw new NotImplementedException();
        protected override Dictionary<uint, ConfigAbility> ConfigAbilityHashMap => throw new NotImplementedException();

        public MonsterAbilityManager(MonsterEntity owner) : base(owner)
        {
        }

        internal override Task HandleAbilityInvokeAsync(AbilityInvokeEntry invoke, Player.Player player)
        {
            return Task.CompletedTask;
        }

        public override void Initialize()
        {

            base.Initialize();
        }
    }
}
