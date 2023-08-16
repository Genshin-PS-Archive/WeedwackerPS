using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(3)]
public class GrowCurveInfo
{
	[ColumnIndex(0)]
	public GrowCurveType type;
	[ColumnIndex(1)]
	public ArithType arith;
	[ColumnIndex(2)]
	public float value;
}