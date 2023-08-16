using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class DungeonRosterConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string openTimeStr;
	[ColumnIndex(2)]
	public uint cycleTime;
	[ColumnIndex(3)]
	public DungeonRosterCycleType cycleType;
	[ColumnIndex(4)][TsvArray(7)]
	public DungeonList[] rosterPool;
}