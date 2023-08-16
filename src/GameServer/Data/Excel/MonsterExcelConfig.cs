using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(30)]
public class MonsterExcelConfig : CreatureExcelConfig
{
	[ColumnIndex(0)]
	public string monsterName;
	[ColumnIndex(1)]
	public MonsterType type;
	[ColumnIndex(3)]
	public MonsterSecurityLevel securityLevel;
	public byte scriptDataPathHashPre;
	public uint scriptDataPathHashSuffix;
	[ColumnIndex(4)]
	public string serverScript;
	[ColumnIndex(5)]
	public string combatConfig;
	//public byte combatConfigHashPre;
	//public uint combatConfigHashSuffix;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[]? affix;
	[ColumnIndex(7)]
	public string ai;
	[ColumnIndex(8)]
	public bool isAIHashCheck;
	[ColumnIndex(9)][TsvArray(2)]
	public uint?[] equips;
	[ColumnIndex(11)]
	public bool canSwim;
	[ColumnIndex(12)][TsvArray(3)]
	public MonsterDrop[] hpDrops;
	[ColumnIndex(18)]
	public uint? killDropId;
	[ColumnIndex(19)]
	public bool isSceneReward;
	[ColumnIndex(20)]
	public VisionLevelType? visionLevel;
	[ColumnIndex(21)]
	public bool isInvisibleReset;
	[ColumnIndex(22)]
	public string excludeWeathers;
	[ColumnIndex(23)]
	public uint featureTagGroupID;
	[ColumnIndex(24)]
	public uint mpPropID;
	[ColumnIndex(25)]
	public string skin;
	[ColumnIndex(26)]
	public uint? describeId;
	[ColumnIndex(27)]
	public bool safetyCheck;
	[ColumnIndex(28)]
	public int? combatBGMLevel;
	[ColumnIndex(29)]
	public int? entityBudgetLevel;

	//not in client
	[ColumnIndex(2)]
	public string description;

	[Columns(2)]
	public class MonsterDrop
	{
		[ColumnIndex(0)]
		public uint? dropId;
		[ColumnIndex(1)]
		public float? hpPercent;
	}
}