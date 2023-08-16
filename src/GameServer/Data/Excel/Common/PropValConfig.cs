using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(2)]
public class PropValConfig
{
	[ColumnIndex(0)]
	public FightPropType? propType;
	[ColumnIndex(1)]
	public float? value;
}