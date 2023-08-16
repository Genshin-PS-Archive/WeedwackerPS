namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class ActivityChessScheduleExcelConfig
{
	[ColumnIndex(0)]
	public uint day;
	[ColumnIndex(1)]
	public uint expUpLimit;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] openMapList;
	[ColumnIndex(3)]
	public uint? condID;
}