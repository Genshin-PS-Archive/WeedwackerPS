using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class ActivityGachaStageExcelConfig
{
	[ColumnIndex(0)]
	public uint stageId;
	[ColumnIndex(1)]
	public uint nextStageId;
	[ColumnIndex(2)]
	public bool isTech;
	[ColumnIndex(3)]
	public ActivityGachaTargetType type;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] groupIdList;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] groupNumList;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[]? stageGroupIdList;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[]? taskContentList;
	[ColumnIndex(8)]
	public uint? condID;
	[ColumnIndex(9)]
	public bool isNeedFinish;
	[ColumnIndex(10)]
	public uint? watcherID;
	[ColumnIndex(11)]
	public uint? openQuestID;
}