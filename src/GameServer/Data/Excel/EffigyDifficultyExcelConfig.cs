using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class EffigyDifficultyExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint challengeId;
	[ColumnIndex(2)]
	public uint baseScore;
	[ColumnIndex(3)]
	public EffigyDifficulty monsterDifficulty;
	[ColumnIndex(4)]
	public uint monsterLevel;
	[ColumnIndex(5)]
	public float scoreRatio;
	[ColumnIndex(6)]
	public uint finishChallengeIndex;
}