namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class CharAmusementOverallExcelConfig
{
	[ColumnIndex(0)]
	public uint scheduleID;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] random_play_list;
	[ColumnIndex(2)]
	public uint dungeonID;
	[ColumnIndex(3)]
	public uint randomLevelCount;
}
