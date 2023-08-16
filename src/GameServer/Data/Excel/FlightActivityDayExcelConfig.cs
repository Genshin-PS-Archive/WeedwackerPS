namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class FlightActivityDayExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public float[] pos;
}