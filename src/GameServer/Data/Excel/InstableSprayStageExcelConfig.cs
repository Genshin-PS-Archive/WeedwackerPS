namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(5)]
public class InstableSprayStageExcelConfig
{
	[ColumnIndex(0)]
	public uint ActivityStage;
	[ColumnIndex(1)]
	public uint OpenDays;
	[ColumnIndex(2)]
	public uint DungeonID;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] LevelID;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] WatcherList;
}
