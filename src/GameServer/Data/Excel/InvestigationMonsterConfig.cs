using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(14)]
public class InvestigationMonsterConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] monsterIdList;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] groupIdList;
	[ColumnIndex(6)]
	public uint? unlockParentQuestId;
	public uint preUnlockParentQuestId;
	public uint[] unlockChapterIdList;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] unlockWqParentQuestIdList;
	public uint wqAcceptHintTextMapHash;
	[ColumnIndex(8)]
	public uint? activeUnlockQuestActivityId;
	[ColumnIndex(9)]
	public uint rewardPreviewId;
	[ColumnIndex(10)]
	public InvestigationMonsterMapMarkCreateType mapMarkCreateType;
	[ColumnIndex(11)]
	public InvestigationMonsterMapMarkCreateCondition mapMarkCreateCondition;
	[ColumnIndex(13)]
	public MonsterCategory monsterCategory;
	public uint nameTextMapHash;
	public string icon;
	public uint descTextMapHash;
	public uint lockDescTextMapHash;
	public uint[] occupiedQuestIdList;

	//not in client
	[ColumnIndex(4)]
	public string groupSurvivalVariable;
	[ColumnIndex(5)]
	public bool trackGroupCenter;

	[Columns(2)]
	public class InvestigationMonsterMapMarkCreateCondition
	{
		[ColumnIndex(0)]
		public InvestigationMonsterMapMarkCreateConditionType ?conditionType;
		[ColumnIndex(1)]
		public uint? conditionParam;
	}
}