namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class BonusTreasureSolutionExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public byte show_imageHashPre;
	public uint show_imageHashSuffix;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] solution;
}