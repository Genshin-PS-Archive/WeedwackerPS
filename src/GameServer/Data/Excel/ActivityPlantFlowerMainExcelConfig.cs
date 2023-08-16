namespace Weedwacker.GameServer.Data.Excel;

[Columns(17)]
public class ActivityPlantFlowerMainExcelConfig
{
	[ColumnIndex(0)]
	public uint scheduleId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] flowerIdList;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] seedIdList;
	[ColumnIndex(4)]
	public uint maxWishFlowerKinds;
	[ColumnIndex(5)]
	public uint rewardPreviewId;
	[ColumnIndex(6)]
	public uint guaranteeStartTimes;
	[ColumnIndex(7)][TsvArray(',')] //guess
	public uint[] preQuestIdList;
	[ColumnIndex(8)]
	public uint openQuestId;
	[ColumnIndex(10)]
	public uint contentDay;
	[ColumnIndex(11)][TsvArray(6)]
	public uint[] dailyConfigIdList;

	//not in client
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] EventSeedID;
	[ColumnIndex(9)]
	public uint editableGroupID;
}