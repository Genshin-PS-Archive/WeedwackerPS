namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class BlessingScanTypeExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint typeId;
	[ColumnIndex(2)]
	public uint priority;
	[ColumnIndex(3)][TsvArray(2)]
	public uint[] upPoolId;
	[ColumnIndex(5)]
	public uint dailyScanLimit;
	public uint typeNameTextMapHash;
	public uint typeNameWithColorTextMapHash;
}