using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(22)]
public class CookRecipeExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint nameTextMapHash;
	public uint rankLevel;
	public string icon;
	public uint descTextMapHash;
	public uint[] effectDesc;
	[ColumnIndex(1)]
	public CookFoodType foodType;
	[ColumnIndex(2)]
	public CookMethodType cookMethod;
	[ColumnIndex(3)]
	public bool isDefaultUnlocked;
	[ColumnIndex(4)]
	public uint maxProficiency;
	[ColumnIndex(5)][TsvArray(3)]
	public IdCountConfig[] qualityOutputVec;
	[ColumnIndex(11)][TsvArray(5)]
	public IdCountConfig[] inputVec;
	[ColumnIndex(21)]
	public string qteParam;
	public uint[] qteQualityWeightVec;
}