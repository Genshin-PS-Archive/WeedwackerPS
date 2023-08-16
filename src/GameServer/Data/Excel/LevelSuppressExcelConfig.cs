namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class LevelSuppressExcelConfig
{
	[ColumnIndex(0)]
	public int level;
	[ColumnIndex(1)]
	public float levelSuppressDamageCo;
	[ColumnIndex(2)]
	public float levelSuppressEndure;
	[ColumnIndex(3)]
	public float levelSuppressDisMinHorizontal;
	[ColumnIndex(4)]
	public float levelSuppressDisMaxHorizontal;
	[ColumnIndex(5)]
	public float levelSuppressDisMinVertical;
	[ColumnIndex(6)]
	public float levelSuppressDisMaxVertical;
	[ColumnIndex(7)]
	public bool isAttackerPlayer;
	[ColumnIndex(8)]
	public bool isDefenserPlayer;
}