using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(55)]
public class DailyTaskExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)]
	public uint poolId;
	[ColumnIndex(6)]
	public DailyTaskType type;
	[ColumnIndex(7)]
	public uint? rarity;
	[ColumnIndex(8)]
	public uint? questId;
	[ColumnIndex(9)][TsvArray(',')]
	public uint[] oldGroupVec;
	[ColumnIndex(10)][TsvArray(',')]
	public uint[] newGroupVec;
	[ColumnIndex(24)]
	public DailyTaskFinishType? finishType;
	[ColumnIndex(25)]
	public uint? finishParam1;
	[ColumnIndex(26)]
	public uint? finishParam2;
	[ColumnIndex(27)]
	public uint? finishProgress;
	[ColumnIndex(52)]
	public uint taskRewardId;
	public string centerPosition;
	public uint enterDistance;
	public uint exitDistance;
	public uint titleTextMapHash;
	public uint descriptionTextMapHash;
	public uint targetTextMapHash;
	public float radarRadius;

	//not in client------------------------------//
	[ColumnIndex(3)][TsvArray(2)]
	public uint?[] taskTag;
	[ColumnIndex(5)]
	public uint taskWeight;
	[ColumnIndex(11)]
	public LogicType? finishCondComb;
	[ColumnIndex(12)][TsvArray(3)]
	public DailyTaskStatisfiedCond[] acceptCond;
	[ColumnIndex(28)][TsvArray(3)]
	public DailyTaskAction[] finishExec;
	[ColumnIndex(53)]
	public uint? taskTypeCycleRefreshTimes;
	[ColumnIndex(54)]
	public DailyTaskTagType tag;

	[Columns(8)]
	public class DailyTaskAction
	{
		[ColumnIndex(0)]
		public DailyTaskCondType? condType;
		[ColumnIndex(1)]
		public int? condParam1;
		[ColumnIndex(2)]
		public int? condParam2;
		[ColumnIndex(3)]
		public int? condParam3;
		[ColumnIndex(4)]
		public DailyTaskActionType? type;
		[ColumnIndex(5)]
		public int? param1;
		[ColumnIndex(6)]
		public int? param2;
		[ColumnIndex(7)]
		public int? param3;
	}
	[Columns(4)]
	public class DailyTaskStatisfiedCond
	{
		[ColumnIndex(0)]
		public ConditionType? type;
		[ColumnIndex(1)]
		public uint? param1;
		[ColumnIndex(2)]
		public uint? param2;
		[ColumnIndex(3)]
		public uint? param3;
	}
	[Columns(1)]
	public class DailyTaskTagType
	{
		[ColumnIndex(0)]
		public uint? value;
	}
}