namespace Weedwacker.GameServer.Data.Excel;

[Columns(19)]
public class FishExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint monsterId;
	[ColumnIndex(2)]
	public uint itemId;
	[ColumnIndex(3)]
	public uint hp;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] skillId;
	[ColumnIndex(6)]
	public uint bonusWidth;
	[ColumnIndex(7)][TsvArray(',')]
	public float[] bonusDuration;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] bonusOffset;
	[ColumnIndex(9)][TsvArray(',')]
	public float[] bonusSpeed;
	[ColumnIndex(10)][TsvArray(',')]
	public float[] feelerTimes;
	[ColumnIndex(11)]
	public float attractRange;
	[ColumnIndex(12)]
	public float fleeRange;
	[ColumnIndex(13)]
	public uint rewardId;
	[ColumnIndex(14)][TsvArray(',')]
	public uint[] dropIdList;
	[ColumnIndex(15)]
	public uint? fishCateId;
	[ColumnIndex(16)]
	public uint biteTimeout;
	[ColumnIndex(17)]
	public uint initPose;
	[ColumnIndex(18)]
	public uint? compoundId;

	//not in client
	[ColumnIndex(5)]
	public uint proficiencyAcceleration;
}