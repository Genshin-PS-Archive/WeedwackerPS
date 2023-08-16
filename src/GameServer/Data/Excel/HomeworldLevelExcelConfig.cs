namespace Weedwacker.GameServer.Data.Excel;

[Columns(14)]
public class HomeworldLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)]
	public uint exp;
	[ColumnIndex(2)]
	public uint comfortPointLimit;
	[ColumnIndex(3)]
	public uint homeCoinStoreLimit;
	[ColumnIndex(4)]
	public uint homeFetterExpStoreLimit;
	[ColumnIndex(5)]
	public uint rewardId;
	[ColumnIndex(6)]
	public uint furnitureMakeSlotCount;
	[ColumnIndex(7)]
	public uint outdoorUnlockBlockCount;
	[ColumnIndex(8)]
	public uint freeUnlockModuleCount;
	[ColumnIndex(9)]
	public uint deployNpcCount;
	[ColumnIndex(10)]
	public uint djinnGadgetId;
	[ColumnIndex(11)]
	public uint limitShopGoodsCount;
	[ColumnIndex(12)]
	public uint limitShopGoodsExtraCount;
	[ColumnIndex(13)][TsvArray(',')]
	public string[] levelFuncs;
}