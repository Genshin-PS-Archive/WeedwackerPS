using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class CoopRewardExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(1)]
	public CoopCondConfig[] rewardCond;
	[ColumnIndex(4)]
	public uint chapterId;
	[ColumnIndex(5)]
	public uint rewardId;
	public uint sortId;
	public uint condTipTextMapHash;
	public uint condTipDesTextMapHash;
}