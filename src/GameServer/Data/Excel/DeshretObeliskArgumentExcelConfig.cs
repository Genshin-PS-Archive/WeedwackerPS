namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(3)]
public class DeshretObeliskArgumentExcelConfig
{
	[ColumnIndex(0)]
	public uint addedValueID;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] groupIdList;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] treasureChestGadgetId;
}
