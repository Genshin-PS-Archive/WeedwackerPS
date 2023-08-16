using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class CityLevelupConfig
{
	[ColumnIndex(0)]
	public uint scene_id;
	[ColumnIndex(1)]
	public uint city_id;
	[ColumnIndex(2)]
	public uint level;
	[ColumnIndex(3)]
	public WorldAreaLevelupConsumeItem consume_item;
	[ColumnIndex(5)]
	public uint? rewardID;
	[ColumnIndex(6)][TsvArray(5)]
	public WorldAreaLevelupAction[] action_vec;
}