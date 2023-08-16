namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class LanV2FireworksStageDataExcelConfig
{
	[ColumnIndex(0)]
	public uint stageId;
	[ColumnIndex(1)]
	public uint openDay;
	[ColumnIndex(2)][TsvArray(',')]
    public uint[] challengeIdList;
	public uint tabNameTextMapHash;
	public uint questDescTextMapHash;
	[ColumnIndex(3)][TsvArray(',')]
    public uint[] guideQuestId;
	public uint questId;
	[ColumnIndex(4)]
	public uint guideQuestRewardPreviewId;
}