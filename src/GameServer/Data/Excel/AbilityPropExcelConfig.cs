namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class AbilityPropExcelConfig
{
	[ColumnIndex(0)]
	public string propName;
	[ColumnIndex(1)]
	public float? overallMin;
	[ColumnIndex(2)]
	public float? overallMax;
	[ColumnIndex(3)]
	public float? limitTagMin;
	[ColumnIndex(4)]
	public float? limitTagMax;
	[ColumnIndex(5)]
	public bool succeedOwner;
}