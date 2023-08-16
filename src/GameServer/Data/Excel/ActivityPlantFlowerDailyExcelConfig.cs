using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class ActivityPlantFlowerDailyExcelConfig
{
	[ColumnIndex(0)]
	public uint dailyConfigId;
	[ColumnIndex(1)][TsvArray(3)]
	public IdCountConfig[] costItemList;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] rewardIdList;
}