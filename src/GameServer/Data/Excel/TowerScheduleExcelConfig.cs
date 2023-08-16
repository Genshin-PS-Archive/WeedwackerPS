namespace Weedwacker.GameServer.Data.Excel;

[Columns(14)]
public class TowerScheduleExcelConfig
{
	[ColumnIndex(0)]
	public uint scheduleId;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] entranceFloorId;
	[ColumnIndex(2)][TsvArray(4)]
	public TowerSchedule[] schedules;
	[ColumnIndex(10)]
	public string closeTime;
	[ColumnIndex(11)]
	public uint rewardGroup;
	public TowerStarReward[] scheduleRewards;
	[ColumnIndex(12)]
	public uint? commemorativeReward;
	[ColumnIndex(13)]
	public uint monthlyLevelConfigId;
	public uint descTextMapHash;
	public uint buffnameTextMapHash;
	public string icon;

	[Columns(2)]
	public class TowerSchedule
	{
		[ColumnIndex(0)][TsvArray(',')]
		public uint[] floorList;
		[ColumnIndex(1)]
		public string openTime;
	}
	public class TowerStarReward
	{
		public uint minStarCount;
		public uint rewardId;
	}
}