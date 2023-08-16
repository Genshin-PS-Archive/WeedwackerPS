using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class HomeWorldLimitShopExcelConfig
{
	[ColumnIndex(0)]
	public uint goodsId;
	[ColumnIndex(1)]
	public uint itemID;
	[ColumnIndex(2)]
	public uint poolID;
	[ColumnIndex(3)][TsvArray(1)]
	public HomeWorldLimitShopCond[] cond;
	[ColumnIndex(5)]
	public uint buyLimit;
	[ColumnIndex(6)][TsvArray(3)]
	public IdCountConfig[] costItems;
	[ColumnIndex(12)]
	public uint weight;

	[Columns(2)]
	public class HomeWorldLimitShopCond
	{
		[ColumnIndex(0)]
		public HomeWorldLimitShopCondType? condition;
		[ColumnIndex(1)][TsvArray(1)]
		public uint?[] conditionParam;
	}
}