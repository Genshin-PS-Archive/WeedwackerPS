namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class AsterMidDifficultyExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] world_level_vec;
	[ColumnIndex(2)]
	public uint drop_id;
	[ColumnIndex(3)]
	public uint reward_id;
	[ColumnIndex(4)]
	public uint resin;
	[ColumnIndex(5)]
	public ushort recommendLevel;
	[ColumnIndex(6)]
	public uint monsterLevel;
}