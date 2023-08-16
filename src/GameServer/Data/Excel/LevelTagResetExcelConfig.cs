namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class LevelTagResetExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)] 
	public uint dungeonId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] seriesIdList;
}