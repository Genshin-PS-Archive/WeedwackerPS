namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class HitLevelTemplateExcelConfig
{
	[ColumnIndex(0)]
	public string type;
	[ColumnIndex(1)]
	public string hitLevel;
	[ColumnIndex(2)]
	public float hitImpulseX;
	[ColumnIndex(3)]
	public float hitImpulseY;
}