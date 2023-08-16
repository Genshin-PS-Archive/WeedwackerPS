namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(4)]
public class DropSubfieldExcelConfig
{
	[ColumnIndex(0)]
	public uint branchDropPoolId;
	[ColumnIndex(1)]
	public uint levelCap;
	[ColumnIndex(2)]
	public uint dropId;
	[ColumnIndex(3)]
	public uint outputSourceType;
}
