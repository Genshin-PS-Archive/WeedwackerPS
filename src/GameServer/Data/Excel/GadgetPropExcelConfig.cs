using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class GadgetPropExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public float hp;
	[ColumnIndex(2)]
	public GrowCurveType hpCurve;
	[ColumnIndex(3)]
	public float attack;
	[ColumnIndex(4)]
	public GrowCurveType attackCurve;
	[ColumnIndex(5)]
	public float defense;
	[ColumnIndex(6)]
	public GrowCurveType defenseCurve;
}