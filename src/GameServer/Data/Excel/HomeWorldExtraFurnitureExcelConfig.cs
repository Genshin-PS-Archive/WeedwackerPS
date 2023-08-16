namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class HomeWorldExtraFurnitureExcelConfig
{
	[ColumnIndex(0)]
	public uint furnitureID;
	[ColumnIndex(1)][TsvArray(5)]
	public string[] extraData;
}