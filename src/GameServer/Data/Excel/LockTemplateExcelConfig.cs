namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class LockTemplateExcelConfig
{
	[ColumnIndex(0)]
	public string type;
	[ColumnIndex(1)]
	public float range;
	[ColumnIndex(2)]
	public float combatPri;
	[ColumnIndex(3)]
	public float normalPri;
}