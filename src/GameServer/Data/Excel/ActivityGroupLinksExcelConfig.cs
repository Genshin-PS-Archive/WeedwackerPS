namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class ActivityGroupLinksExcelConfig
{
	[ColumnIndex(0)]
	public uint linkId;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] setList;
}