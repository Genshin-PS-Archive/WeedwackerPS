namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(3)]
public class GravenCarveStageExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint incrementPlacedWoodcuts;
	[ColumnIndex(2)]
	public uint watcher;
}
