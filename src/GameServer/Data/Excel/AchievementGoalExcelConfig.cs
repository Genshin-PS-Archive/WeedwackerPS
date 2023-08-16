namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class AchievementGoalExcelConfig
{
	public uint id;
	[ColumnIndex(0)]
	public uint orderId;
	public uint nameTextMapHash;
	[ColumnIndex(1)]
	public uint? finishRewardId;
	public string iconPath;
}