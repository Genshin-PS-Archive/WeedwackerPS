namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class BlessingScanPicExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint grantRewardCost;
	public string iconName;
	public uint nameTextMapHash;
}