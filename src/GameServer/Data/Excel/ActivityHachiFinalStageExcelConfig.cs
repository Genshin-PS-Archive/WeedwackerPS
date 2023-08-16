namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class ActivityHachiFinalStageExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public uint stageId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] questId;
	public uint questTitleTextMapHash;
	public uint questDescTextMapHash;
	public uint bossTitleTextMapHash;
	public uint bossDescTextMapHash;
	[ColumnIndex(3)]
	public uint dungeonId;
	[ColumnIndex(4)][TsvArray(';')]
	public uint?[] watcherIdList;
	[ColumnIndex(5)]
	public uint openDay;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] finalQuestID;
	public uint dungeonEntryId;
}