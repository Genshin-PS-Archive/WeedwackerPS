using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(35)]
public class ShopGoodsData
{
	[ColumnIndex(0)]
	public uint goodsId;
	public uint subTagNameTextMapHash;
	public uint subTabId;
	[ColumnIndex(1)]
	public uint shopType;
	[ColumnIndex(2)]
	public uint itemId;
	[ColumnIndex(3)]
	public uint? rotateId;
	public uint showId;
	[ColumnIndex(4)]
	public uint itemCount;
	[ColumnIndex(5)]
	public uint? costScoin;
	[ColumnIndex(6)]
	public uint? costHcoin;
	[ColumnIndex(7)]
	public uint? costMcoin;
	public float discountRate;
	public uint originalPrice;
	[ColumnIndex(8)][TsvArray(4)]
	public IdCountConfig[] costItems;
	[ColumnIndex(16)]
	public uint? buyLimit;
	[ColumnIndex(17)]
	public ShopRefreshType? refreshType;
	[ColumnIndex(18)]
	public uint? refreshParam;
	[ColumnIndex(19)]
	public uint? displayDaysBeforeSell;
	[ColumnIndex(20)]
	public string beginTime;
	[ColumnIndex(21)]
	public string endTime;
	[ColumnIndex(22)]
	public bool isBuyOnce;
	[ColumnIndex(23)]
	public ShopPrecondition? precondition;
	[ColumnIndex(24)]
	public uint? preconditionParam;
	[ColumnIndex(25)][TsvArray(',')]
	public string[] preconditionParamList;
	[ColumnIndex(26)][TsvArray(',')]
	public string[] preconditionParamList2;
	[ColumnIndex(27)]
	public bool preconditionHidden;
	[ColumnIndex(28)]
	public uint minShowLevel;
	[ColumnIndex(29)]
	public uint minPlayerLevel;
	[ColumnIndex(30)]
	public uint maxPlayerLevel;
	[ColumnIndex(31)]
	public uint sortLevel;
	[ColumnIndex(32)]
	public uint? secondarySheetId;
	[ColumnIndex(33)]
	public uint? chooseOneGroupId;
	[ColumnIndex(34)][TsvArray(',')]
	public uint?[] platformTypeList;
}
