using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(3)]
public class WorldAreaLevelupAction
{
	[ColumnIndex(0)]
	public WorldAreaLevelupActionType? type;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] param1_vec;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] param2_vec;
}