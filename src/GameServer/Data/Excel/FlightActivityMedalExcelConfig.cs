namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class FlightActivityMedalExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public string medal_icon;
	[ColumnIndex(1)][TsvArray(7)]
	public FlightDailyInfo[] daily_info;

	[Columns(1)]
	public class FlightDailyInfo
	{
		[ColumnIndex(0)]
		public uint watcher;
	}
}