namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class FleurFairPreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	[ColumnIndex(2)]
	public uint activityStayTime;
	[ColumnIndex(3)]
	public uint unlockQuestID;
	[ColumnIndex(4)]
	public uint unlockPlayerLevel;
	[ColumnIndex(5)]
	public uint gameplayPreQuest;
	[ColumnIndex(6)]
	public uint rewardPreview;
}