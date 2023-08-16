namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class BlitzRushExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	[ColumnIndex(2)]
	public uint contentDuration;
	[ColumnIndex(3)]
	public uint dungeonId; //4 stages
	[ColumnIndex(4)]
	public uint dungeonEntryId;
	[ColumnIndex(5)]
	public uint dungeonRewardId;
	[ColumnIndex(6)]
	public uint prePreQuestID;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] preQuestID;
	[ColumnIndex(8)]
	public uint rewardPreview;
	public bool hideOathEntrance;
}