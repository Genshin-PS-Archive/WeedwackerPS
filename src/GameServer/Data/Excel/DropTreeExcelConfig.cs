using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(96)]
public class DropTreeExcelConfig : BaseDropIndexConfig
{
	[ColumnIndex(0)][TsvArray(30)]
	public DropNode[] subDrops;
	[ColumnIndex(90)]
	public DropNodeType type;
	[ColumnIndex(91)]
	public bool fallToGround;
	[ColumnIndex(92)]
	public uint? outputSourceType;
	[ColumnIndex(93)]
	public uint? dailyOutputLimit;
	[ColumnIndex(94)]
	public uint? totalOutputLimit;
	[ColumnIndex(95)]
	public uint? activityOutputLimit;
}
