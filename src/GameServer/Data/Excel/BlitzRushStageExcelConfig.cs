namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class BlitzRushStageExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint openDay;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] watcherIdList;
	public uint stageTitleTextMapHash;
	public uint gamePlayDescTextMapHash;
	public uint guidetaskDescTextMapHash;
	[ColumnIndex(3)]
	public uint guideQuestID;
	public uint pushTipsId;
}