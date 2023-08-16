namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class ChapterExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint? beginQuestId;
	[ColumnIndex(2)]
	public uint? endQuestId;
	[ColumnIndex(3)]
	public uint? needPlayerLevel; //AR
	[ColumnIndex(6)]
	public uint? activityId;
	[ColumnIndex(4)]
	public string needBeginTimeStr;
	public uint chapterNumTextMapHash;
	public uint chapterTitleTextMapHash;
	public string chapterIcon;
	public byte chapterImageHashPre;
	public uint chapterImageHashSuffix;
	public uint chapterImageTitleTextMapHash;
	[ColumnIndex(7)]
	public uint? inActivityNeedPlayerLevel;
	public string chapterSerialNumberIcon;

	//not in client
	[ColumnIndex(5)]
	public bool hideChapterIcon;
}