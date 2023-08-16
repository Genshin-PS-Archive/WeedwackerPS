namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class BlitzRushParkourExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint openDay;
	[ColumnIndex(2)]
	public uint groupId;
	public uint titleTextMapHash;
	public uint descTextMapHash;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] watcherIdList;
}