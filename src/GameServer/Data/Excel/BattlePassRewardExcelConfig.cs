namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class BattlePassRewardExcelConfig
{
	[ColumnIndex(0)]
	public uint indexId;
	[ColumnIndex(1)]
	public uint level;
	[ColumnIndex(2)][TsvArray(1)]
	public uint[] freeRewardIdList;
	[ColumnIndex(3)][TsvArray(2)]
	public uint[] paidRewardIdList;
	[ColumnIndex(5)]
	public bool showUp;
}