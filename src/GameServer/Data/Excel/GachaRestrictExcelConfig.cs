namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(5)]
public class GachaRestrictExcelConfig
{
	[ColumnIndex(0)]
	public uint GachaPoolID;
	[ColumnIndex(1)]
	public uint UniversalLimitLimit;
	[ColumnIndex(2)]
	public uint GeneralLimitDisplayNumber;
	[ColumnIndex(3)]
	public uint MaximumLimitForMinors; // :skull:
	[ColumnIndex(4)]
	public uint GeneralLimitDisplayNumberForMinors;
}
