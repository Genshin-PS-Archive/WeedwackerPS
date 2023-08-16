namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class BlossomOpenExcelConfig
{
	[ColumnIndex(0)]
	public uint cityId;
	[ColumnIndex(1)]
	public uint openLevel;
}