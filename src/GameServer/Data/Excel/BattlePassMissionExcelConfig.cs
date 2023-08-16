using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class BattlePassMissionExcelConfig : WatcherConfig
{
	[ColumnIndex(0)]
	public BattlePassMissionRefreshType refreshType;
	[ColumnIndex(1)]
	public bool isForce;
	[ColumnIndex(2)]
	public uint addPoint;
	[ColumnIndex(3)]
	public uint? scheduleId;
	public uint activityId;
	public uint descTextMapHash;
	public QuestGuide guide;
}