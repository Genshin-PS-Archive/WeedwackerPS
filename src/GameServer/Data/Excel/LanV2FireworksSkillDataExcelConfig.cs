using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class LanV2FireworksSkillDataExcelConfig
{
	[ColumnIndex(0)]
	public uint skillId;
	[ColumnIndex(1)]
	public FireworksReformSkillType skillType;
	[ColumnIndex(2)]
	public uint staminaValueCost;
	[ColumnIndex(3)]
	public uint unlockChallengeId;
	[ColumnIndex(4)]
	public int deltaFireElementValue;
	[ColumnIndex(5)]
	public uint luckyProb;
	[ColumnIndex(6)]
	public uint factorAddValueMin;
	[ColumnIndex(7)]
	public uint factorAddValueMax;
	[ColumnIndex(8)][TsvArray(3)]
	public uint[] effectParams;
	public uint skillTitleTextMapHash;
	public uint skillDescTextMapHash;
	public uint[] descArgs;
}