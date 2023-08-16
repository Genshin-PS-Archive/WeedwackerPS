using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class CombineExcelConfig
{
	[ColumnIndex(0)]
	public uint combineId;
	[ColumnIndex(1)]
	public uint playerLevel;
	[ColumnIndex(2)]
	public bool isDefaultShow;
	[ColumnIndex(6)]
	public uint combineType;
	public uint subCombineType;
	[ColumnIndex(7)]
	public uint resultItemId;
	[ColumnIndex(8)]
	public uint resultItemCount;
	[ColumnIndex(9)]
	public uint scoinCost;
	[ColumnIndex(10)][TsvArray(1)]
	public RandomItemConfig[] randomItems;
	[ColumnIndex(13)][TsvArray(3)]
	public IdCountConfig[] materialItems;
	public uint effectDescTextMapHash;
	[ColumnIndex(19)]
	public RecipeType recipeType;

	//not in client
	[ColumnIndex(3)]
	public uint? unlockType;
	[ColumnIndex(4)][TsvArray(2)]
	public uint?[] unlockParams;
	[ColumnIndex(20)][TsvArray(',')]
	public uint[] specialResultItemId;
}