namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class ActivityDeliveryExcelConfig
{
	[ColumnIndex(0)]
	public uint scheduleId;
	[ColumnIndex(1)][TsvArray(7)]
	public uint[] dailyConfigIdList;
	[ColumnIndex(8)]
	public uint needPlayerLevel;
}