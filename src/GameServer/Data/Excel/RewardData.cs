namespace Weedwacker.GameServer.Data.Excel;

[Columns(29)]
public class RewardData : OutputControlConfig
{
	[ColumnIndex(0)]
	public uint rewardId;
	[ColumnIndex(1)]
	public uint? hcoin;
	[ColumnIndex(2)]
	public uint? scoin; //mora
	[ColumnIndex(3)]
	public uint? playerExp; //adventure exp
	[ColumnIndex(4)]
	public uint? avatarExp;
	[ColumnIndex(5)]
	public uint? fetterExp;
	[ColumnIndex(6)]
	public uint? resin;
	[ColumnIndex(7)][TsvArray(9)]
	public RewardItemConfig[] rewardItemList;

	//not in client
	[ColumnIndex(25)]
	public string outputSourceType;
	[ColumnIndex(26)]
	public uint? dailyOutputLimit;
	[ColumnIndex(27)]
	public uint? historyOutputLimit;
	[ColumnIndex(28)]
	public uint? activityOutputLimit;

	[Columns(2)]
	public class RewardItemConfig
	{
		[ColumnIndex(0)]
		public uint? itemId;
		[ColumnIndex(1)]
		public uint? itemCount;
	}
}

[Columns(0)]
public abstract class OutputControlConfig
{
}