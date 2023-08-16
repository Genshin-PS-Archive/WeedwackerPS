namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class BooksCodexExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public uint materialId;
	[ColumnIndex(2)]
	public uint SortOrder;
	[ColumnIndex(3)]
	public bool isDisuse;
}