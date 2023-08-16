namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class ExploreAreaTotalExpExcelConfig
{
	[ColumnIndex(0)]
	public uint areaID;
	[ColumnIndex(1)]
	public uint totalExp;
	[ColumnIndex(2)]
	public float? reputationRatio;
}