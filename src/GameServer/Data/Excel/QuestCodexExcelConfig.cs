namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class QuestCodexExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public uint parentQuestId;
	[ColumnIndex(2)]
	public uint chapterId;
	[ColumnIndex(3)]
	public uint SortOrder;
	[ColumnIndex(4)]
	public bool isDisuse;
}