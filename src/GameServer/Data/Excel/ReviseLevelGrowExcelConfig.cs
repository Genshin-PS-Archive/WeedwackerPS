namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class ReviseLevelGrowExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(9)]
	public uint[] grade;
}