using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class EffigyLimitingConditionExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint descTextMapHash;
	[ColumnIndex(1)]
	public EffigyCondition conditionType;
	public string icon;
	[ColumnIndex(2)]
	public uint conditionParam1;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] conditionParam2;
	[ColumnIndex(4)]
	public uint? exclusiveId;
	[ColumnIndex(5)]
	public bool isInRow;
	public uint exclusiveDescTextMapHash;
	[ColumnIndex(6)]
	public int score;
}