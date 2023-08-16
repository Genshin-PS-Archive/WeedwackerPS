namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class BlossomSectionOrderExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)]
	public uint sectionId;
	[ColumnIndex(3)]
	public uint order;
}