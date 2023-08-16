namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class BounceConjuringPreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	[ColumnIndex(2)]
	public uint activityStayTime;
	public uint preQuestId;
	public uint questWatcherID;
	public uint rewardPreviewId;
	public uint matchID;
}