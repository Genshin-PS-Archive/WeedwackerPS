namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class ReputationLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
    [ColumnIndex(1)]
    public uint level;
    [ColumnIndex(2)]
    public uint cityId;
	public uint levelNameTextMapHash;
    [ColumnIndex(3)]
    public uint nextLevelExp;
    [ColumnIndex(4)]
    public uint rewardId;
    [ColumnIndex(5)]
    public uint requestGroupId;
    [ColumnIndex(6)]
    public uint requestNum;
    [ColumnIndex(7)]
    public uint requestAcceptNum;
	public uint functionId;
}