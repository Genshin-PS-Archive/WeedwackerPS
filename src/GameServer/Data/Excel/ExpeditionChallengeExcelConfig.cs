namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class ExpeditionChallengeExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint unlockTime;
	[ColumnIndex(2)]
	public uint groupId;
	[ColumnIndex(3)]
	public uint rewardChallengeIndex;
	[ColumnIndex(4)]
	public uint rewardId;
	public uint challengeNameTextMapHash;
	public uint challengeDescTextMapHash;
	public string superElement;
	public float[] centerPosList;
	public uint centerRadius;
}