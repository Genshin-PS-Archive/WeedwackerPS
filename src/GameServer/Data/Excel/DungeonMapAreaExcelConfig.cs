namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class DungeonMapAreaExcelConfig
{
	[ColumnIndex(0)]
	public uint dungeonID;
	[ColumnIndex(1)]
	public uint areaID;
}