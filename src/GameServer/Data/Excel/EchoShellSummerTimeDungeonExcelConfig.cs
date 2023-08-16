namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class EchoShellSummerTimeDungeonExcelConfig
{
	[ColumnIndex(0)]
	public uint dungeonId;
	[ColumnIndex(1)]
	public uint totalChestCount;
	[ColumnIndex(2)]
	public uint totalShellCount;
}