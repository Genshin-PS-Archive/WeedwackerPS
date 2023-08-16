namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class AsterMidExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] camp_vec;
}