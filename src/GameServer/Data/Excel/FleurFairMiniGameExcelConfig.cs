using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class FleurFairMiniGameExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public FleurFairMiniGameType miniGameType;
	public uint descTextMapHash;
	[ColumnIndex(2)]
	public uint galleryId;
	[ColumnIndex(3)]
	public uint configId;
	[ColumnIndex(4)]
	public uint openDay;
	[ColumnIndex(5)]
	public uint? questId;
	public uint rewardRankLowTextMapHash;
	public uint rewardRankMiddleTextMapHash;
	public uint rewardRankHighTextMapHash;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] watcherIdList;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] avatarIdList;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] avatarScore;
	[ColumnIndex(9)][TsvArray(',')]
	public uint[] tipsIdList;
	public uint tipsDescTextMapHash;
	public float[] pos;
}