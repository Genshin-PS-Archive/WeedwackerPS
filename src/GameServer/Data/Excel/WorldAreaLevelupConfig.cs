using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class WorldAreaLevelupConfig
{
	public uint scene_id;
	public uint area_id;
	public uint level;
	public WorldAreaLevelupConsumeItem consume_item;
	public uint rewardID;
	public WorldAreaLevelupAction[] action_vec;
}