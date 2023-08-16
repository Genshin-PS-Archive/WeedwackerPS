namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class AsterStageExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	[ColumnIndex(2)]
	public uint chapterId;
	[ColumnIndex(3)]
	public string titleText;
	//public uint titleTextMapHash;
	[ColumnIndex(4)]
	public string stageName;
	//public uint stageNameTextMapHash;
	[ColumnIndex(5)]
	public uint openday;
	[ColumnIndex(6)]
	public uint openQuestId;
	[ColumnIndex(7)]
	public string introText;
	//public uint introTextMapHash;
	[ColumnIndex(8)]
	public string storyText;
	//public uint storyTextMapHash;
	[ColumnIndex(9)]
	public uint? unlockScore;
	[ColumnIndex(10)][TsvArray(',')]
	public uint[] questIdList;
	[ColumnIndex(11)]
	public uint? rewardPreviewId;
}