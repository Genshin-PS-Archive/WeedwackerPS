namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class BartenderBasicExcelConfig
{
	[ColumnIndex(0)]
	public uint activityId;
	public uint prequestID;
	public uint chapterID;
	public uint furnitureRewardWatcher;
	public uint furnitureRewardID;
	public uint namecardRewardWatcher;
	public uint namecardRewardID;
	[ColumnIndex(1)]
	public uint minMaterialCount;
	[ColumnIndex(2)]
	public uint maxMaterialCount;
	[ColumnIndex(4)]
	public uint npcMarkID; //talk
	public uint challengeOpenDay;
	public uint challengeOpenCondID;
	public uint storyOpenCondID;
	public uint mainEndingQuestID;

	//not in client
	[ColumnIndex(3)]
	public uint sceneID;
}