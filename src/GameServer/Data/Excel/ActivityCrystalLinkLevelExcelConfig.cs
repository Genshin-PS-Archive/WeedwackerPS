using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class ActivityCrystalLinkLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint levelId;
	[ColumnIndex(1)]
	public uint scheduleId;
	[ColumnIndex(2)]
	public uint openDay;
	[ColumnIndex(3)]
	public uint dungeonId;
	[ColumnIndex(4)][TsvArray(';')]
	public uint[] trialAvatarList;
	[ColumnIndex(5)][TsvArray(';')]
	public uint[] watcherIdList;
	[ColumnIndex(6)][TsvArray(';')]
	public uint[] condBuffIdList;
	[ColumnIndex(7)][TsvArray(';')]
	public uint[] effectBuffIdList;
	public uint levelTitleTextMapHash;
	public uint levelDescTextMapHash;
	public MonsterConfig[] monsterConfigArray;
	public MainMonsterConfig[] mainMonsterConfigArray;
	public uint[] scoreLevelList;
	public uint condCD;
	public uint effLastTime;

	public class MainMonsterConfig
	{
		public uint[] boss;
		public uint[] monster;
	}
}