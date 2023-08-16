namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(8)]
public class EffigyChallengeV2ExcelConfig
{
	[ColumnIndex(0)]
	public uint ConfigID;
	[ColumnIndex(1)]
	public uint OpenDayIndex;
	[ColumnIndex(2)]
	public uint PassThroughDungeonID;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] PassThroughGalleryIDList;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] PassThroughtSkillIDList;
	[ColumnIndex(5)]
	public uint ChallengeDungeonID;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] ChallengeGalleryIDList;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] ChallengeSkillIDList;
}
