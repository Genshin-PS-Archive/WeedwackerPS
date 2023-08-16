namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(2)]
public class DropSubfieldEntry
{
	[ColumnIndex(0)]
	public string name;
	[ColumnIndex(1)]
	public uint? dropSubfieldId;
}