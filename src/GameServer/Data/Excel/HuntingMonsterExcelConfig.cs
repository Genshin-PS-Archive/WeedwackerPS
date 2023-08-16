using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(19)]
public class HuntingMonsterExcelConfig
{
	[ColumnIndex(0)]
	public uint configId;
	[ColumnIndex(1)]
	public uint monsterId;
	[ColumnIndex(2)]
	public uint? associatedMonsterGroupId;
	[ColumnIndex(3)]
	public HuntingMonsterFinishType? huntingFinishType;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] affix;
	[ColumnIndex(5)]
	public string abilityGroup;
	[ColumnIndex(6)]
	public HuntingMonsterCreatePosType? createPosType;
	[ColumnIndex(7)]
	public uint level;
	[ColumnIndex(8)]
	public uint reviseLevelId;
	[ColumnIndex(9)][TsvArray(',')]
	public uint[] cityList;
	[ColumnIndex(10)]
	public HuntingOfferDifficultyType difficulty;
	[ColumnIndex(11)]
	public uint limitTime;
	[ColumnIndex(12)]
	public uint? searchPointNum;
	[ColumnIndex(13)][TsvArray(2)]
	public HuntingRefreshCond[] refreshCond;
	[ColumnIndex(15)]
	public uint? initialPose;
	public uint[] clueTextIdList;
	public uint newsTextTextMapHash;
	public uint traitTextTextMapHash;
	public uint mechanismTitle1TextMapHash;
	public uint mechanismDesc1TextMapHash;
	public uint mechanismTitle2TextMapHash;
	public uint mechanismDesc2TextMapHash;
	[ColumnIndex(16)][TsvArray(',')]
	public uint[] suiteId;
	[ColumnIndex(17)]
	public bool isDisableWandering;
	[ColumnIndex(18)]
	public uint? routeId;

	[Columns(1)]
	public class HuntingRefreshCond
	{
		[ColumnIndex(0)]
		public HuntingRefreshCondType? condType;
		public uint[] param;
	}
}