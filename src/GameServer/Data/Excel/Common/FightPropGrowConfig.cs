using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(2)]
public class FightPropGrowConfig
{
	[ColumnIndex(0)]
	public FightPropType? type;
	[ColumnIndex(1)]
	public GrowCurveType? grow_curve;
}