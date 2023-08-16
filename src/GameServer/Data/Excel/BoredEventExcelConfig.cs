using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class BoredEventExcelConfig
{
	[ColumnIndex(0)]
	public BoardEventType event_type;
	[ColumnIndex(1)]
	public uint param;
	[ColumnIndex(2)]
	public bool is_enable;
	[ColumnIndex(3)]
	public int add_bored;
	[ColumnIndex(4)]
	public int max_bored;
}