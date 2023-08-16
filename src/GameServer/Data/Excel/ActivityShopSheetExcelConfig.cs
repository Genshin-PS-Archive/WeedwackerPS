using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class ActivityShopSheetExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public bool isAheadPreview;
	[ColumnIndex(2)]
	public LogicType? condComb;
	public uint SheetNameTextMapHash;
	[ColumnIndex(3)][TsvArray(2)]
	public ActivityShopSheetCond[] cond;
	public uint[] sortLevel;

	[Columns(2)]
	public class ActivityShopSheetCond
	{
		[ColumnIndex(0)]
		public ActivityShopSheetCondType? type;
		[ColumnIndex(1)][TsvArray(',')]
		public uint[]? param;
	}
}