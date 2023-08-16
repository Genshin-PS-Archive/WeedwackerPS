namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class BattlePassLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)]
	public uint levelUpNeedPoint;
}