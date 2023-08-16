using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Data.BinOut.Gadget;
using Weedwacker.GameServer.Data.BinOut.Shared.ConfigEntity;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Ability
{
    internal class GadgetAbilityManager : BaseAbilityManager
    {
		private BaseGadgetEntity GadgetOwner => Owner as BaseGadgetEntity;
        private readonly ConfigGadget Config;
        private readonly ConfigGadget? ItemConfig;
		public override Dictionary<uint, Dictionary<uint, object>?>? AbilitySpecials { get; } = new();
        public override Dictionary<string, HashSet<string>> UnlockedTalentParams => throw new NotImplementedException();
        protected override Dictionary<uint, ConfigAbility> ConfigAbilityHashMap { get; } = new();

        public GadgetAbilityManager(BaseGadgetEntity owner) : base(owner)
        {
			Config = GameData.ConfigGadgetMap[GadgetOwner.Data.jsonName];
            if(GadgetOwner.Data.itemJsonName != null)
			    ItemConfig = GameData.ConfigGadgetMap.GetValueOrDefault(GadgetOwner.Data.itemJsonName, null);
		}

        public override void Initialize()
        {
            if(Config.abilities != null)
            {
                AddConfigEntityAbilityEntries(Config.abilities);
				uint instanceCount = 1;
				foreach (ConfigEntityAbilityEntry ability in Config.abilities)
				{
					AddAbility(new AbilityAppliedAbility()
					{
						AbilityName = new AbilityString() { Hash = Utils.AbilityHash(ability.abilityName) },
						InstancedAbilityId = instanceCount++,
						AbilityOverride = ability.abilityOverride == null ? null : new AbilityString() { Hash = Utils.AbilityHash(ability.abilityOverride) }
					});
				}
			}

            if(ItemConfig != null && ItemConfig.abilities != null)
            {
                AddConfigEntityAbilityEntries(ItemConfig.abilities);
			}

            base.Initialize();
        }
    }
}
