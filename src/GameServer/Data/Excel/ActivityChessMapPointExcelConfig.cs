namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class ActivityChessMapPointExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	public float coordX;
	public float coordY;
}