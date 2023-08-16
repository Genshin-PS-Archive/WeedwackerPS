namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class ReunionScheduleExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activateLevel;
	[ColumnIndex(2)]
	public uint firstGiftRewardId;
	[ColumnIndex(3)]
	public uint dailySignInId;
	[ColumnIndex(4)]
	public uint reunionMissionId;
	[ColumnIndex(5)]
	public uint reunionPrivilegeId;
}