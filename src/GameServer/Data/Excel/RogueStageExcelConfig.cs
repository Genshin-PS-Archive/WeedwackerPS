namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class RogueStageExcelConfig
{
    [ColumnIndex(0)]
    public uint stageId;
	[ColumnIndex(1)]
	public uint firstPassRewardId;
	public uint previewRewardId;
	[ColumnIndex(2)]
	public uint openTime;
	[ColumnIndex(3)]
	public uint maxCoin;
	[ColumnIndex(4)]
	public uint maxLevel;
	[ColumnIndex(5)]
	public uint reviseLevelId;
	public uint stageNameTextMapHash;
	public uint stageDescTextMapHash;
	public uint[] bossIdList;
	public uint[][] monsterIdList;
	public uint[][] levelBossIdList;
	public uint preQuestId;
	public uint gotoQuestId;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] unlockRuneList;
	public bool isStoryStage;
    // not in client
    [ColumnIndex(6)]
    public uint unlockConditions;
    [ColumnIndex(8)]
    public uint SSRGuaranteedTimes;

}