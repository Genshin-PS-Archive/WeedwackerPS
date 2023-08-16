namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class DailyDungeonConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(';')]
	public uint[] monday;
	[ColumnIndex(2)][TsvArray(';')]
	public uint[] tuesday;
	[ColumnIndex(3)][TsvArray(';')]
	public uint[] wednesday;
	[ColumnIndex(4)][TsvArray(';')]
	public uint[] thursday;
	[ColumnIndex(5)][TsvArray(';')]
	public uint[] friday;
	[ColumnIndex(6)][TsvArray(';')]
	public uint[] saturday;
	[ColumnIndex(7)][TsvArray(';', ',')]
	public uint?[] sunday;
}