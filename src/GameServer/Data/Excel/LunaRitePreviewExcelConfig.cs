namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class LunaRitePreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public uint activityId;
	public uint unlockQuestId;
	public uint unlockQuestId2;
	public uint unlockPlayerLevel;
	public uint rewardId;
	public uint activityQuestId;
	public uint guideQuestId;
	public uint challengePushTipsID;
	public uint plotPushTipsID;
	public uint plotPushTipsPreQuestID;
	public uint maxAtmosphere;
}