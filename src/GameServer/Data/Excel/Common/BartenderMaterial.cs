namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(2)]
public class BartenderMaterial
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint? count;
}