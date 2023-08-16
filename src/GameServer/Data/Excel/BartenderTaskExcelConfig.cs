namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class BartenderTaskExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint unlockDay;
	[ColumnIndex(1)]
	public uint parentQuestId;
	public uint unlockCond;
	public uint avatarID1;
	public uint avatarID2;
	[ColumnIndex(2)]
	public uint rewardPreviewId;
	public uint nameTextMapHash;
	public uint descTextMapHash;
}