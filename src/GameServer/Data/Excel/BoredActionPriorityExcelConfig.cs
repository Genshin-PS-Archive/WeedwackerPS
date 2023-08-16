using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class BoredActionPriorityExcelConfig
{
	[ColumnIndex(0)]
	public BoredActionType action_type;
	[ColumnIndex(1)]
	public uint weight;
}