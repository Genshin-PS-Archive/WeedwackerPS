namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(1)]
public class DungeonList
{
	[ColumnIndex(0)][TsvArray(',')]
	public uint[] dungeonList;
}