namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class LevelTagMapAreaConfig
{
	[ColumnIndex(0)]
	public uint LevelTagID;
	[ColumnIndex(1)]
	public uint MapAreaID;
	[ColumnIndex(2)]
	public int index;
}