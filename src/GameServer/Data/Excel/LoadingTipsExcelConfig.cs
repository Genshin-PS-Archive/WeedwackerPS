namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class LoadingTipsExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint tipsTitleTextMapHash;
	public uint tipsDescTextMapHash;
	[ColumnIndex(1)]
	public string stageID;
	[ColumnIndex(2)]
	public string startTime;
	[ColumnIndex(3)]
	public string endTime;
	[ColumnIndex(4)]
	public uint minLevel;
	[ColumnIndex(5)]
	public uint maxLevel;
	[ColumnIndex(6)]
	public string limitOpenState;
	[ColumnIndex(7)]
	public string preMainQuestIds;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] preQuestIdList;
	[ColumnIndex(9)][TsvArray(',')]
    public uint[] disableQuestIdList;
	[ColumnIndex(10)][TsvArray(',')]
    public uint[] enableDungeonId;
	[ColumnIndex(11)]
	public uint weight;
}