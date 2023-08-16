namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class FeatureTagGroupExcelConfig
{
	[ColumnIndex(0)]
	public uint groupID;
	[ColumnIndex(1)][TsvArray(20)]
	public uint?[] tagIDs;
}