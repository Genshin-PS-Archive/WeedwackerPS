namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class HuntingRegionExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(',')]
	public float[] centerPosList;
	[ColumnIndex(2)]
	public uint centerRadius;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] safeClueGroup;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] clueGroup;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] safeDestinationGroup;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] destinationGroup;
	public uint regionInfoTextMapHash;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] certainFinalMonsterId;
}