namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class ExploreExcelConfig
{
	[ColumnIndex(0)]
	public uint materialID;
	[ColumnIndex(1)]
	public uint exp;
}