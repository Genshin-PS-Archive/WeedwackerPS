namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class FleurFairChapterExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint chapterId;
	[ColumnIndex(2)]
	public uint openDay;
	[ColumnIndex(3)]
	public string titleText;
	//public uint titleTextMapHash;
	[ColumnIndex(4)]
	public string descText;
	//public uint descTextMapHash;
	[ColumnIndex(5)]
	public uint mainQuest;
	[ColumnIndex(6)]
	public uint? preQuest;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] miniQuest;
	[ColumnIndex(8)]
	public string icon;
}