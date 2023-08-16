namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(10)]
public class GachaRuleExcelConfig
{
	[ColumnIndex(0)]
	public uint RuleID;
	[ColumnIndex(1)]
	public uint Priority;
	[ColumnIndex(2)]
	public uint GuaranteeType;
	[ColumnIndex(3)]
	public uint GuaranteeTriggerNumber;
	[ColumnIndex(4)][TsvArray(4)]
	public uint?[] GuaranteeParam;
	[ColumnIndex(8)]
	public uint? GuaranteeResetType;
	[ColumnIndex(9)]
	public uint? GuaranteeResetParam;
}
