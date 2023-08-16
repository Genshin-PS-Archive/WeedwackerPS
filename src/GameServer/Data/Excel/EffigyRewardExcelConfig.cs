namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class EffigyRewardExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint scoreGrade;
	[ColumnIndex(2)]
	public uint rewardID;
}