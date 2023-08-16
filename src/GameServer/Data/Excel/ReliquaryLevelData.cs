using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(43)]
public class ReliquaryLevelData
{
	[ColumnIndex(0)]
	public uint rank;
	[ColumnIndex(1)]
	public uint level;
	[ColumnIndex(2)]
	public uint exp;
	[ColumnIndex(3)][TsvArray(20)]
	public PropValConfig[] addProps;
}
