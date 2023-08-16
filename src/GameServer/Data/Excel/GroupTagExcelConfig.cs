namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class GroupTagExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string tagName;

	//not in client
	[ColumnIndex(2)]
	public string tagEnumeration;
}