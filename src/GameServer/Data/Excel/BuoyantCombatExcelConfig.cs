namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class BuoyantCombatExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	public uint rewardPreviewId;
	public uint pushTipsId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] levelIdList;
}