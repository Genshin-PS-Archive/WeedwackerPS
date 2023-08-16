using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(18)]
public class EntityDropSubfieldExcelConfig
{
	[ColumnIndex(0)]
	public uint EntityId;
	[ColumnIndex(1)]
	public uint type;
	[ColumnIndex(2)][TsvArray(7)]
	public DropSubfieldEntry[] BranchDrops;
	[ColumnIndex(16)]
	public uint dropCap;
	[ColumnIndex(17)]
	public uint? dailyDropCap;
}
