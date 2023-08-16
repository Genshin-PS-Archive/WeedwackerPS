namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(3)]
public class DeathRegionLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint DeathZoneLevel;
	[ColumnIndex(1)]
	public uint baseErosion;
	[ColumnIndex(2)]
	public int erosionRate;
}
