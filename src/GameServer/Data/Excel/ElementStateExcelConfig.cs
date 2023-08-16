using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class ElementStateExcelConfig
{
	[ColumnIndex(0)]
	public ElementType elementType;
	public string elementIcon;
	[ColumnIndex(1)]
	public uint rank;
}