namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(5)]
public class BaseDropIndexConfig
{
	[ColumnIndex(1)]
	public uint? minLevel;
	[ColumnIndex(4)]
	public string dropTag;
	[ColumnIndex(0)]
	public uint dropId;
	public uint dropCount;

	//not in client
	[ColumnIndex(2)]
	public bool maxLevel; //?????
	[ColumnIndex(3)]
	public bool randomDirection;
}