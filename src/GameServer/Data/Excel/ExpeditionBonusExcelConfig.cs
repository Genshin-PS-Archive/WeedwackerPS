namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class ExpeditionBonusExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint avatarLevel;
	[ColumnIndex(2)]
	public float probability;
}