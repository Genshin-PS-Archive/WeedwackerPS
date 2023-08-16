namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class ReputationQuestExcelConfig
{
	[ColumnIndex(0)]
	public uint ParentQuestId;
    [ColumnIndex(1)]
    public uint cityId;
    [ColumnIndex(2)]
    public uint rewardId;
	public string iconName;
	public uint titleTextMapHash;
	public uint order;
}