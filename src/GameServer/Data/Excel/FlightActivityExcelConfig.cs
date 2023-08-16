namespace Weedwacker.GameServer.Data.Excel;

[Columns(20)]
public class FlightActivityExcelConfig
{
	[ColumnIndex(1)]
	public uint id;
	[ColumnIndex(0)]
	public uint activity_id;
	[ColumnIndex(2)]
	public uint preview_reward_id;
	public uint start_quest_id;
	public uint npc_id;
	[ColumnIndex(3)][TsvArray(3)]
	public uint[] medal_id;
	[ColumnIndex(6)][TsvArray(7)]
	public FlightDailyPointFactor[] daily_factor_vec;

	[Columns(2)]
	public class FlightDailyPointFactor
	{
		[ColumnIndex(0)]
		public uint time_factor;
		[ColumnIndex(1)]
		public uint gold_factor;
	}
}