namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class EffigyChallengeExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint dayIndex;
	[ColumnIndex(2)]
	public uint dungeonId;
	public string prefab;
	public uint titleTextMapHash;
	public uint contentTextMapHash;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] limitingConditionVec;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] upAvatarVec;
	[ColumnIndex(5)]
	public uint firstPassRewardId;
}