namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class RogueCellWeightExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint springWeight;
	[ColumnIndex(2)]
	public uint storeWeight;
	[ColumnIndex(3)]
	public uint eliteWeight;
}