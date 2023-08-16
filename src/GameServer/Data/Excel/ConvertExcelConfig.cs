using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(18)]
public class ConvertExcelConfig
{
	[ColumnIndex(0)]
	public uint ConvertID;
	[ColumnIndex(1)]
	public uint? unlockLevel;
	[ColumnIndex(2)]
	public uint defaultDisplay;
	[ColumnIndex(3)]
	public uint? unlockMethod;
	[ColumnIndex(4)][TsvArray(2)]
	public uint?[] unlockParams;
	[ColumnIndex(6)]
	public uint convertType;
	[ColumnIndex(7)]
	public uint convertResult;
	[ColumnIndex(8)]
	public uint convertQuantity;
	[ColumnIndex(9)]
	public uint moraCost;
	[ColumnIndex(10)][TsvArray(3)]
	public IdCountConfig[] materials;
	[ColumnIndex(16)]
	public uint recipeType;
	[ColumnIndex(17)]
	public uint? specialProductId;
}
