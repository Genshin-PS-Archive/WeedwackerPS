namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class ActivityChessPreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint activityID;
	public uint descTextMapHash;
	public uint rewardPreviewID;
	public uint preQuestMainID;
	public uint openQuestMainID;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] watcherList;
	[ColumnIndex(2)]
	public uint matchPlayerLimit;
	[ColumnIndex(3)]
	public bool openTeachDungeon;
	[ColumnIndex(4)]
	public uint teachDungeonID;
	[ColumnIndex(5)]
	public uint teachMapID;
	[ColumnIndex(6)]
	public uint punishTime;
	public uint coinID;
	[ColumnIndex(7)]
	public uint seriesID;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] teachScoreIdList;
}