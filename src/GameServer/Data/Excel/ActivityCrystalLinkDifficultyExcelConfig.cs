using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class ActivityCrystalLinkDifficultyExcelConfig
{
	[ColumnIndex(0)]
	public uint difficultyId;
	[ColumnIndex(1)]
	public uint scheduleId;
	[ColumnIndex(2)]
	public uint dungeonLevel;
	[ColumnIndex(3)]
	public float scoreRatio;
	public uint monsterLevel;
	public CrystalLinkDifficultyType difficulty;
	public uint descTextMapHash;
}