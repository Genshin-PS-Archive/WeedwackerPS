namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class TowerFloorExcelConfig
{
	[ColumnIndex(0)]
	public uint floorId;
	[ColumnIndex(1)]
	public uint floorIndex;
	[ColumnIndex(2)]
	public uint levelGroupId;
	[ColumnIndex(3)]
	public uint overrideMonsterLevel;
	[ColumnIndex(4)]
	public uint teamNum;
	public uint rewardIdFiveStars;
	public uint rewardIdTenStars;
	public uint rewardIdFifteenStars;
	public uint rewardIdThreeStars;
	public uint rewardIdSixStars;
	public uint rewardIdNineStars;
	[ColumnIndex(5)]
	public uint unlockStarCount;
	[ColumnIndex(6)]
	public uint floorLevelConfigId; //buffs and debuffs
	public string bgImage;
}