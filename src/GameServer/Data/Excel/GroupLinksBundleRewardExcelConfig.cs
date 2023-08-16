namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class GroupLinksBundleRewardExcelConfig
{
	[ColumnIndex(0)]
	public uint rewardId;
	[ColumnIndex(1)]
	public uint? rewardPreviewID;
	[ColumnIndex(2)]
	public uint? dropID;
}