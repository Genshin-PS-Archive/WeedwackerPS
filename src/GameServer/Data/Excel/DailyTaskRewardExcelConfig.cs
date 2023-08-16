namespace Weedwacker.GameServer.Data.Excel;

[Columns(25)]
public class DailyTaskRewardExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)][TsvArray(12)]
	public DailyTaskDropConfig[] dropVec;

	[Columns(2)]
	public class DailyTaskDropConfig
	{
		[ColumnIndex(0)]
		public uint drop_id;
		[ColumnIndex(1)]
		public uint preview_reward_id;
	}
}