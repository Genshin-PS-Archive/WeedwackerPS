namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class CityTaskOpenExcelConfig
{
	[ColumnIndex(0)]
	public uint cityId;
	[ColumnIndex(1)]
	public uint questId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] backupVec;
}