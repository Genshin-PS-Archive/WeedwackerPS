﻿using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Systems.Player;

namespace Weedwacker.GameServer.Systems.Ability
{
    internal class TeamAbilityManager : BaseAbilityManager
    {
        public TeamAbilityManager(TeamManager teamManager) : base(teamManager)
        {
        }

        protected override Dictionary<uint, ConfigAbility> ConfigAbilityHashMap { get => throw new NotImplementedException(); }
        public override Dictionary<string, HashSet<string>> UnlockedTalentParams => throw new NotImplementedException();
        public override Dictionary<uint, Dictionary<uint, object>?>? AbilitySpecials => (Owner as TeamManager).CurrentTeamInfo.AbilitySpecials;
    }
}
