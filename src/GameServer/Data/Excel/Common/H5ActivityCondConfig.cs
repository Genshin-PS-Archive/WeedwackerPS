using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(2)]
public class H5ActivityCondConfig
{
	[ColumnIndex(0)]
	public H5ActivityCondType? type;
	[ColumnIndex(1)]
	public string? paramStr;
}