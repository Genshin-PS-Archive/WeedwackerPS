
namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class BoredCreateMonsterExcelConfig
{
	[ColumnIndex(0)]
	public uint player_level;
	[ColumnIndex(1)][TsvArray(3)]
	public BoredMonsterConfig[] monster_config_vec;

	[Columns(2)]
	public class BoredMonsterConfig
	{
		[ColumnIndex(0)]
		public uint? weight;
		[ColumnIndex(1)]
		public uint? id;
	}
}