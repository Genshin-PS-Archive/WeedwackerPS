using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

public class ActivitySumoDifficultyExcelConfig
{
	public uint Id;
	public uint scheduldId;
	public SumoDifficultyType difficulty;
	public uint monsterLevel;
	public uint dungeonLevel;
	public float ratio;
	public uint descTextMapHash;
}