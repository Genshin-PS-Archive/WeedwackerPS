namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(8)]
public class GachaProbExcelConfig
{
	[ColumnIndex(0)]
	public uint ProbabilityRuleID;
	[ColumnIndex(1)]
	public uint PropParentType;
	[ColumnIndex(2)]
	public uint PropType;
	[ColumnIndex(3)]
	public uint? RoundTablePriority;
	[ColumnIndex(4)]
	public uint? BaseProbability;
	[ColumnIndex(5)]
	public bool IsGuaranteed;
	[ColumnIndex(6)]
	public uint? InitialGuaranteeNumber;
	[ColumnIndex(7)]
	public uint? GuaranteeSingleIncreaseProbability;
}
