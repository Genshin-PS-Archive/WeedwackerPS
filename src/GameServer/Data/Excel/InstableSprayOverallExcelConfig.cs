namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(3)]
public class InstableSprayOverallExcelConfig
{
	[ColumnIndex(0)]
	public uint ActivityID;
	[ColumnIndex(1)]
	public uint ScheduleID;
	[ColumnIndex(2)]
	public uint BuffDropWeight;
}
