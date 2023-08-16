namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(6)]
public class CatalogExcelConfig
{
	[ColumnIndex(0)]
	public uint catalogID;
	[ColumnIndex(1)]
	public uint catalogType;
	[ColumnIndex(2)][TsvArray(3)]
	public TabList[] tabList;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] syncWatcherList;

	[Columns(1)]
	public class TabList
	{
		[ColumnIndex(0)][TsvArray(',')]
		public uint[] tabs;
	}
}
