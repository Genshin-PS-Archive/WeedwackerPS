using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(14)]
public class FishSkillExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public FishSkillCategory skillCategory;
	[ColumnIndex(2)]
	public FishSkillType skillType;
	[ColumnIndex(3)][TsvArray(3)]
	public float?[] param;
	[ColumnIndex(6)]
	public uint strength;
	[ColumnIndex(7)]
	public float forceFactor;
	[ColumnIndex(8)][TsvArray(',')]
	public float[] bonusWidth;
	[ColumnIndex(9)][TsvArray(',')]
	public float[] bonusDuration;
	[ColumnIndex(10)][TsvArray(',')]
	public uint[] bonusOffset;
	[ColumnIndex(11)][TsvArray(',')]
	public float[] bonusSpeed;
	[ColumnIndex(12)]
	public float duration;
	[ColumnIndex(13)]
	public uint priority;
}