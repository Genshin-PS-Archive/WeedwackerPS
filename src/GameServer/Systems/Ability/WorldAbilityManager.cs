﻿using Weedwacker.GameServer.Data.BinOut.Ability.Temp;

namespace Weedwacker.GameServer.Systems.Ability
{
    internal class WorldAbilityManager : BaseAbilityManager
    {
        protected override Dictionary<uint, ConfigAbility> ConfigAbilityHashMap => throw new NotImplementedException();

        public override Dictionary<uint, Dictionary<uint, object>?>? AbilitySpecials => throw new NotImplementedException();
        public override Dictionary<string, HashSet<string>> UnlockedTalentParams => throw new NotImplementedException();

        public WorldAbilityManager(World.World world) : base(world)
        {
        }
    }
}
