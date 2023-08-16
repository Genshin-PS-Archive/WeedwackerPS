namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class BoredMonsterPoolConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint monster_id;
	[ColumnIndex(2)]
	public uint level;
	[ColumnIndex(3)]
	public string dropTag;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] affixVec;
	[ColumnIndex(5)]
	public bool isElite;
}