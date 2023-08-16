using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class BlossomChestData
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint chestGadgetId;
	[ColumnIndex(2)]
	public uint? worldResin;
	[ColumnIndex(3)]
	public uint resin;
	[ColumnIndex(4)]
	public BlossomRefreshType refreshType;
	[ColumnIndex(5)]
	public BlossomChestShowType clientShowType;
}
