namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class CustomLevelGroupConfig
{
	[ColumnIndex(0)]
	public uint groupId;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] dungeonList;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] activityDungeonList;
	[ColumnIndex(3)]
	public uint maxEditCount;
	[ColumnIndex(4)]
	public uint storeMaxCount;
	[ColumnIndex(5)]
	public uint coinChallengeId;
	[ColumnIndex(6)]
	public uint coinExhibitionId;
	[ColumnIndex(7)]
	public uint finishExhibitionId;
	[ColumnIndex(8)]
	public uint costAlert;
}