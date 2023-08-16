using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class ReliquaryMainPropData
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint prop_depot_id;
	[ColumnIndex(2)]
	public FightPropType propType;
	public ReliquaryMainAffixName affixName;

	//not in client
	[ColumnIndex(3)]
	public uint randomWeight;
}
