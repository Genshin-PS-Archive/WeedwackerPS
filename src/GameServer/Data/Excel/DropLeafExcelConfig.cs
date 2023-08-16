using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(81)]
public class DropLeafExcelConfig : BaseDropIndexConfig
{
	[ColumnIndex(0)][TsvArray(25)]
	public DropNode[] subDrops;
	[ColumnIndex(75)]
	public DropNodeType type;
	[ColumnIndex(76)]
	public bool fallToGround;
	[ColumnIndex(77)]
	public uint? outputSourceType;
	[ColumnIndex(78)]
	public uint? dailyOutputLimit;
	[ColumnIndex(79)]
	public uint? totalOutputLimit;
	[ColumnIndex(80)]
	public uint? activityOutputLimit;
}
