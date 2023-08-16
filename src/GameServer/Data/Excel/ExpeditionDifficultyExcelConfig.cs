namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class ExpeditionDifficultyExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint needHours;
	[ColumnIndex(2)]
	public uint minPlayer;
	[ColumnIndex(3)]
	public uint maxPlayer;
	[ColumnIndex(4)]
	public uint minRefreshCount;
	[ColumnIndex(5)]
	public uint maxRefreshCount;
	[ColumnIndex(6)]
	public float coef;
}