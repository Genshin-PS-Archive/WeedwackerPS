namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class ExpeditionPathExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint difficultyId;
	[ColumnIndex(2)]
	public string superElement;
	[ColumnIndex(3)]
	public uint basicRewardId;
	[ColumnIndex(4)]
	public uint bonusRewardId;
	public uint pathNameTextMapHash;
	public uint pathDescTextMapHash;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] levelRewardList;
	public uint rewardPreviewId;
}