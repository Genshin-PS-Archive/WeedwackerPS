namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class DungeonSerialConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint MaxTakeNum;
	[ColumnIndex(2)]
	public uint? takeCost;
}