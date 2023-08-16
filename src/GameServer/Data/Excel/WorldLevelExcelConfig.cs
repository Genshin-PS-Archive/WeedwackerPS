namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class WorldLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)]
	public uint monsterLevel;
}