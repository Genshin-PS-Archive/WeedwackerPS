namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class FleurFairDungeonExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint sectionId;
	[ColumnIndex(2)]
	public uint openDay;
	[ColumnIndex(3)]
	public uint obtainCoinLimit;
	[ColumnIndex(4)]
	public uint activityId;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] watcherIdList;
	[ColumnIndex(6)]
	public uint dungeonId;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] miniGameList;
	[ColumnIndex(11)][TsvArray(',')]
	public uint[] monsterList;
	[ColumnIndex(12)][TsvArray(',')]
	public uint[] energyGradeList;
	[ColumnIndex(13)]
	public uint initialEnergy;
	[ColumnIndex(14)]
	public uint energyLimit;
	[ColumnIndex(18)]
	public uint successRewardId;
	[ColumnIndex(19)]
	public uint failureRewardId;
	[ColumnIndex(20)]
	public uint punishTime;
	public uint titleTextMapHash;
	public uint descTextMapHash;

	//not in client
	[ColumnIndex(8)][TsvDictionary(':', ';')]
	public Dictionary<uint, uint> miniGameConfig1;
	[ColumnIndex(9)][TsvDictionary(':', ';')]
	public Dictionary<uint, uint> miniGameConfig2;
	[ColumnIndex(10)][TsvDictionary(':', ';')]
	public Dictionary<uint, uint> miniGameConfig3;

	[ColumnIndex(15)][TsvArray(',')]
	public uint[] abilityGroupConfig1;
	[ColumnIndex(16)][TsvArray(',')]
	public uint[] abilityGroupConfig2;
	[ColumnIndex(17)][TsvArray(',')]
	public uint[] abilityGroupConfig3;
}