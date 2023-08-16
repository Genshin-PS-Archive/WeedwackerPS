using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class ConstValueExcelConfig
{
	[ColumnIndex(0)]
	public ConstValueType name;
	[ColumnIndex(1)][TsvArray(6)]
	public string?[] value;
}