namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class HomeWorldShopSubTagExcelConfig
{
	[ColumnIndex(0)]
	public uint subID;
	[ColumnIndex(1)]
	public bool showNewMark;
	[ColumnIndex(2)]
	public string subTagText;
	//public uint subTagTextMapHash;
}