using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp.Predicates;

public class ByCurTeamWeaponTypeSort : ConfigAbilityPredicate
{
	public uint number;
	public RelationalOperator logic;
}