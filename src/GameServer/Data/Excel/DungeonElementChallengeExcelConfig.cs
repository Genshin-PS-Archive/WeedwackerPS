namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class DungeonElementChallengeExcelConfig
{
	[ColumnIndex(0)]
	public uint dungeonId;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] trialAvatarId;
	public uint tutorialId;
}