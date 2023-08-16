using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class ShopData
{
	[ColumnIndex(1)]
	public uint shopId;
	[ColumnIndex(0)]
	public ShopType shopType;
	[ColumnIndex(2)]
	public ShopRefreshType? refreshType;
	[ColumnIndex(3)]
	public uint? refreshParam;
	[ColumnIndex(4)]
	public OpenStateType? openStateType;
	[ColumnIndex(5)]
	public uint? cityId;
	[ColumnIndex(6)]
	public uint? cityDiscountLevel;
	[ColumnIndex(7)]
	public uint? scoinDiscountRate;
	public uint vipFuncID;
}
