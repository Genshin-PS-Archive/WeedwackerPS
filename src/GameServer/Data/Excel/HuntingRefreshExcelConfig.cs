using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class HuntingRefreshExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)]
	public HuntingOfferDifficultyType difficulty;
	[ColumnIndex(3)]
	public uint regionId;
	[ColumnIndex(4)]
	public uint finishRewardId;
}