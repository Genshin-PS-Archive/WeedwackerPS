namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(6)]
public class ChestDropExcelConfig
{
	[ColumnIndex(0)]
	public uint minimum_level;
	[ColumnIndex(1)]
	public string generalIndex;
	[ColumnIndex(2)]
	public uint dropID;
	[ColumnIndex(3)]
	public uint dropCount;
	[ColumnIndex(4)]
	public uint outputSourceType;
	[ColumnIndex(5)]
	public string category;
}
