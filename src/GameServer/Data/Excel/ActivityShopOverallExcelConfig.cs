using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class ActivityShopOverallExcelConfig
{
	[ColumnIndex(0)]
	public uint scheduleId;
	[ColumnIndex(1)]
	public ShopType shopType;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] sheetList;
}