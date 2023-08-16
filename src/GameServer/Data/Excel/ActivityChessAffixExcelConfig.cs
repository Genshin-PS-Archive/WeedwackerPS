namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class ActivityChessAffixExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	public uint nameTextMapHash;
	public uint descTextMapHash;
}