namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(3)]
public class DropNode
{
	[ColumnIndex(0)]
	public uint? id;
	[ColumnIndex(1)]
	public string numInterval;
	[ColumnIndex(2)]
	public uint? weight;
}