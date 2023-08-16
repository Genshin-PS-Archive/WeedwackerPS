using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(26)]
public class ForgeExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint playerLevel;
	[ColumnIndex(2)]
	public bool isDefaultShow;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] effectiveWorldLevels;
	[ColumnIndex(7)]
	public uint forgeType;
	public uint showItemId;
	public uint showConsumeItemId;
	[ColumnIndex(8)]
	public uint? resultItemId;
	[ColumnIndex(9)]
	public uint resultItemCount;
	[ColumnIndex(10)]
	public uint? mainRandomDropId;
	public uint mainForgeRandomId;
	[ColumnIndex(11)]
	public uint forgeTime;
	[ColumnIndex(12)]
	public uint queueNum;
	[ColumnIndex(13)]
	public uint scoinCost;
	[ColumnIndex(14)][TsvArray(1)]
	public RandomItemConfig[] randomItems;
	[ColumnIndex(17)][TsvArray(3)]
	public IdCountConfig[] materialItems;
	[ColumnIndex(23)]
	public uint priority;
	[ColumnIndex(24)]
	public uint forgePoint;
	public uint forgePointNoticeTextMapHash;

	//not in client
	[ColumnIndex(3)]
	public uint? unlockType;
	[ColumnIndex(4)][TsvArray(2)]
	public uint?[] unlockParams;
	[ColumnIndex(25)][TsvArray(',')]
	public uint[] talentReturnId;
}