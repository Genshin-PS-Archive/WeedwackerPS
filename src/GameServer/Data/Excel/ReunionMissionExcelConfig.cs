namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class ReunionMissionExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
    [ColumnIndex(1)]
    public uint watcherGroupId;
    [ColumnIndex(2)]
    public uint targetScore;
    [ColumnIndex(3)]
    public uint finishRewardId;
    [ColumnIndex(4)][TsvArray(',')]
    public uint[] targetScoreList;
    [ColumnIndex(5)][TsvArray(',')]
    public uint[] finishRewardIdList;
    [ColumnIndex(6)][TsvArray(',')]
    public uint[] showRewardIdList;

    //not in client
    [ColumnIndex(7)]
    public string awardPresentationPrefeb;
}