namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class ReliquaryCodexExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public uint suitId;
	[ColumnIndex(2)]
	public uint level;
	[ColumnIndex(3)]
	public uint? cupId;
	[ColumnIndex(4)]
	public uint? leatherId;
	[ColumnIndex(5)]
	public uint capId;
	[ColumnIndex(6)]
	public uint? flowerId;
	[ColumnIndex(7)]
	public uint? sandId;
	[ColumnIndex(8)]
	public uint SortOrder;
	[ColumnIndex(9)]
	public bool isDisuse;
	[ColumnIndex(10)]
	public bool showOnlyUnlocked;
}