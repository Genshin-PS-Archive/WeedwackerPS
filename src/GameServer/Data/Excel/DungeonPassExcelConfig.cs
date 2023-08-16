using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(18)]
public class DungeonPassExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public LogicType? logicType;
	[ColumnIndex(2)][TsvArray(4)]
	public DungeonCondConfig[] conds;

	[Columns(4)]
	public class DungeonCondConfig
	{
		[ColumnIndex(0)]
		public DungeonCondType? condType;
		[ColumnIndex(1)][TsvArray(3)]
		public int?[] param;
	}
}