namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class PersonalLineExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint titleTextMapHash;
	[ColumnIndex(1)]
	public uint chapterId;
	[ColumnIndex(2)]
	public uint startQuestId;
	public uint avatarId;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] preQuestId;
	[ColumnIndex(4)]
	public string startTime;
	public uint descTextMapHash;
	public uint sortOrder;
}