namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class FishRodExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public float baseAttack;
	[ColumnIndex(2)]
	public float attackAcc;
	[ColumnIndex(3)]
	public float maxAttack;
	[ColumnIndex(4)]
	public uint cityId;
	[ColumnIndex(5)]
	public float attackMag;
}