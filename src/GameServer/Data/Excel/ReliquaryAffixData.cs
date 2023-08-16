using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class ReliquaryAffixData
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint depot_id;
	[ColumnIndex(2)]
	public uint group_id;
	[ColumnIndex(3)]
	public FightPropType propType;
	[ColumnIndex(4)]
	public float propValue;

	//not in client
	[ColumnIndex(5)]
	public uint randomWeight;
	[ColumnIndex(6)]
	public uint upgradeWeight;
}
