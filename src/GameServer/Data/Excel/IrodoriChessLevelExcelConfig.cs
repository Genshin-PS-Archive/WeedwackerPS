namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class IrodoriChessLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint levelId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] watcherList;
	[ColumnIndex(3)]
	public uint normalMapId;
	[ColumnIndex(4)]
	public uint hardMapId;
	[ColumnIndex(5)]
	public uint unlockHardScore;

	//not in client
	[ColumnIndex(1)]
	public uint openDays;
}