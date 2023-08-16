using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(18)]
public class TowerLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint levelId;
	[ColumnIndex(1)]
	public uint levelGroupId;
	[ColumnIndex(2)]
	public uint levelIndex;
	[ColumnIndex(3)]
	public uint dungeonId;
	[ColumnIndex(4)][TsvArray(3)]
	public TowerCondition[] conds;
	[ColumnIndex(13)][TsvArray(3)]
	public string[] towerBuffConfigStrList;
	public uint dailyRewardId;
	public uint firstPassRewardId;
	[ColumnIndex(16)]
	public uint monsterLevel;
	public uint[] firstMonsterList;
	public uint[] secondMonsterList;

	//not in client
	[ColumnIndex(17)]
	public bool isSplit;

	[Columns(3)]
	public class TowerCondition
	{
		[ColumnIndex(0)]
		public TowerCondType towerCondType;
		[ColumnIndex(1)][TsvArray(',')]
		public uint[] argumentListUpper;
		[ColumnIndex(2)][TsvArray(',')]
		public uint[] argumentList;
	}
}