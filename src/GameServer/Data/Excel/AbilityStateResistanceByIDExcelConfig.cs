using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class AbilityStateResistanceByIDExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(20)]
	public AbilityState[] abilityStates;
}