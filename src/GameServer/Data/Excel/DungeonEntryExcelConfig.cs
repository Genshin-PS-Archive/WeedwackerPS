using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(16)]
public class DungeonEntryExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint sceneId;
	[ColumnIndex(2)]
	public uint dungeon_entry_id;
	[ColumnIndex(3)]
	public DungunEntryType type;
	[ColumnIndex(4)]
	public bool isShowInAdvHandbook;
	public uint descTextMapHash;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] cooldown_tips_dungeon_id;
	[ColumnIndex(6)]
	public bool is_default_open;
	public bool is_daily_refresh;
	[ColumnIndex(7)]
	public LogicType? cond_comb;
	[ColumnIndex(8)][TsvArray(2)]
	public DungeonEntrySatisfiedCond[] satisfied_cond;
	public string pic_path;
	[ColumnIndex(14)]
	public uint? system_open_ui_id;
	[ColumnIndex(15)]
	public uint? reward_data_id;
	public uint[][] description_cycle_reward_list;


	[Columns(3)]
	public class DungeonEntrySatisfiedCond
	{
		[ColumnIndex(0)]
		public DungeonEntrySatisfiedConditionType? type;
		[ColumnIndex(1)]
		public uint? param1;
		[ColumnIndex(2)]
		public uint? param2;
	}
}