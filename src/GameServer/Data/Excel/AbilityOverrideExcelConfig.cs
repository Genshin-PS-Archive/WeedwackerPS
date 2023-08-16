namespace Weedwacker.GameServer.Data.Excel;

[Columns(15)]
public class AbilityOverrideExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string abilityName;
	[ColumnIndex(2)][TsvArray(5)]
	public AbilityOverrideParamConfig[] abilitySpecialParamConfig;
	[ColumnIndex(12)][TsvArray(',')]
	public string[] paramNameList;
	[ColumnIndex(13)][TsvArray(',')]
	public float[]? paramValueList;
	[ColumnIndex(14)]
	public bool resetAbilitySpecial;

	[Columns(2)]
	public class AbilityOverrideParamConfig
	{
		[ColumnIndex(0)]
		public string? paramName;
		[ColumnIndex(1)]
		public float? paramValue;
	}
}