namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class ElementCoeffExcelConfig
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)]
	public float crashCo;
	[ColumnIndex(2)]
	public float elementLevelCo;
	[ColumnIndex(3)]
	public float playerElementLevelCo;
	[ColumnIndex(4)]
	public float playerShieldLevelCo;
}