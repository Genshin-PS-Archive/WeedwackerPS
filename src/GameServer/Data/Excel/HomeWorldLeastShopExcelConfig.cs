namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class HomeWorldLeastShopExcelConfig
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)]
	public uint poolID;
	[ColumnIndex(2)]
	public uint count;
}