namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(3)]
public class RandomItemConfig
{
	[ColumnIndex(0)]
	public uint? itemId;
	[ColumnIndex(1)]
	public uint? count;
	[ColumnIndex(2)]
	public uint? weight;
}