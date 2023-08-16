namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(3)]
public class GachaWishExcelConfig
{
	[ColumnIndex(0)]
	public uint GachaPoolID;
	[ColumnIndex(1)]
	public uint WishingParentModule;
	[ColumnIndex(2)]
	public uint WishPointLimit;
}
