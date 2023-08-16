namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class ReputationRequestExcelConfig
{
	[ColumnIndex(0)]
	public uint RequestId;
    [ColumnIndex(1)]
    public uint QuestId;
    [ColumnIndex(2)]
    public uint GroupId;
    [ColumnIndex(3)]
    public uint weight;
    [ColumnIndex(4)]
    public uint npcId;
    [ColumnIndex(5)]
    public uint rewardId;
	public uint nameTextMapHash;
	public uint descTextMapHash;
	public string iconName;
}