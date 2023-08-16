namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class AsterActivityPreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint activityId;
	public uint descTextMapHash;
	public uint unlockLevel;
	public uint rewardPreviewId;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] watcherList;
	[ColumnIndex(2)]
	public uint specialRewardId;
	[ColumnIndex(3)]
	public uint activityStayTime;
	public uint perfabChangeQuestId;
}