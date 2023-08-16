using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(14)]
public class FurnitureMakeExcelConfig
{
	[ColumnIndex(0)]
	public uint configID;
	[ColumnIndex(1)]
	public uint furnitureItemID;
	[ColumnIndex(2)]
	public uint count;
	[ColumnIndex(3)]
	public uint exp; //first time only
	[ColumnIndex(4)][TsvArray(3)]
	public IdCountConfig[] materialItems;
	[ColumnIndex(10)]
	public uint makeTime;
	[ColumnIndex(11)]
	public uint maxAccelerateTime;
	[ColumnIndex(12)]
	public uint quickFetchMaterialNum;

	//not in client
	[ColumnIndex(13)]
	public uint? talentNoReturnId;
}