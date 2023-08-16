using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(34)]
public class BlossomRefreshExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint nameTextMapHash;
	public string icon;
	public uint descTextMapHash;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)]
	public BlossomRefreshType refreshType;
	[ColumnIndex(3)]
	public uint refreshCount;
	[ColumnIndex(4)]
	public string refreshTime;
	[ColumnIndex(5)]
	public OpenStateType? openState;
	[ColumnIndex(6)]
	public uint? openLevel;
	[ColumnIndex(7)]
	public uint? closeLevel;
	[ColumnIndex(8)][TsvArray(5)]
	public BlossomRefreshCond[] refreshCondVec;
	[ColumnIndex(18)]
	public uint reviseLevel;
	[ColumnIndex(19)]
	public ItemLimitType? itemLimitType;
	[ColumnIndex(20)]
	public uint? blossom_chest_id;
	[ColumnIndex(21)]
	public uint? camp_update_need_count;
	[ColumnIndex(22)]
	public uint roundMaxCount;
	[ColumnIndex(23)][TsvArray(9)]
	public BlossomRewardExcelConfig[] dropVec;
	[ColumnIndex(31)]
	public BlossomShowType clientShowType;
	[ColumnIndex(32)]
	public bool hideBG;
	[ColumnIndex(33)]
	public BlossomRewardType rewardType;


	[Columns(2)]
	public class BlossomRefreshCond
	{
		[ColumnIndex(0)]
		public BlossomRefreshCondType? type;
		[ColumnIndex(1)][TsvArray(',')]
		public uint[]? param;
	}
	[Columns(2)]
	public class BlossomRewardExcelConfig
	{
		[ColumnIndex(0)]
		public uint drop_id;
		[ColumnIndex(1)]
		public uint? preview_reward;
	}
}