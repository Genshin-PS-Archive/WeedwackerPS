using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class BattlePassScheduleExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint titleNameTextMapHash;
	[ColumnIndex(1)]
	public string beginDateStr;
	[ColumnIndex(2)]
	public string endDateStr;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] cycleList;
	[ColumnIndex(4)]
	public uint extraPaidRewardId;
	[ColumnIndex(5)]
	public uint extraPaidAddPoint;
	[ColumnIndex(6)]
	public uint buyLevelCostCoinNum;
	[ColumnIndex(7)]
	public uint cyclePointUpperLimit;
	[ColumnIndex(9)]
	public uint levelRewardIndexId;
	public uint[] normalRewardList;
	public BattlePassScheduleRewardType rewardType;
	public string showImage;
	public uint[] showRewardList;
	public uint[] stroyRewardList;
	public uint storyId;
	[ColumnIndex(11)]
	public uint? mailDayCount;
	[ColumnIndex(12)]
	public uint? mailConfigId;

	//not in client
	[ColumnIndex(8)]
	public uint backpackOverflowEmailID;
	[ColumnIndex(10)]
	public uint endRewardEmailID;
}