namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(6)]
public class FungusBaseExcelConfig
{
	[ColumnIndex(0)]
	public uint activityId;
	[ColumnIndex(1)]
	public uint skillUsageMaxNum;
	[ColumnIndex(2)]
	public uint skillUsageInitialNum;
	[ColumnIndex(3)]
	public uint mushroomBeastInBattleNum;
	[ColumnIndex(4)]
	public string skillSyncSGV;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] resourcePackList;
}
