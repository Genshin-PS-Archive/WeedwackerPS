namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(2)]
public class WorldAreaLevelupConsumeItem
{
	[ColumnIndex(0)]
	public uint? item_id;
	[ColumnIndex(1)]
	public uint? item_num;
}