namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class ReputationExploreExcelConfig
{
	[ColumnIndex(0)]
	public uint exploreId;
    [ColumnIndex(1)]
    public uint cityId;
    [ColumnIndex(2)]
    public uint exploreProgress;
    [ColumnIndex(3)]
    public uint rewardId;
	public uint conditionTextTextMapHash;
}