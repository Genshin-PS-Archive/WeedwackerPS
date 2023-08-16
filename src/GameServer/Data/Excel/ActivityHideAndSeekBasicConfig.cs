namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class ActivityHideAndSeekBasicConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityID;
	public uint titleTextMapHash;
	public uint descTextMapHash;
	public uint pushTipsID;
	[ColumnIndex(2)] //big guess
	public uint? rewardPreviewID;
	public uint oneTimeRewardPreviewID;
	public uint unlockQuestID;
	[ColumnIndex(3)]
	public uint matchID;
	[ColumnIndex(4)]
	public uint draftID;
	public uint[] scoreUnlockList;
	public uint[] watcherList;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] skillList;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] mapList;
	public uint[] chanllengeList;
	[ColumnIndex(7)]
	public uint scoreItemID;

	//not in client
	[ColumnIndex(8)][TsvArray(',')]
	public float[] eventEndTransportPoint1;
	[ColumnIndex(9)][TsvArray(',')]
	public float[] eventEndTransportPoint2;
	[ColumnIndex(10)][TsvArray(',')]
	public float[] eventEndTransportPoint3;
}