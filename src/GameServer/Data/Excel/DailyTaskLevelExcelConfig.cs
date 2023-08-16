namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class DailyTaskLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint minPlayerLevel;
	[ColumnIndex(2)]
	public uint maxPlayerLevel;
	[ColumnIndex(3)]
	public uint groupReviseLevel;
	[ColumnIndex(4)]
	public uint scoreDropId;
	[ColumnIndex(5)]
	public uint scorePreviewRewardId;
}