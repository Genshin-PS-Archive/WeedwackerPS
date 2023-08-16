namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class AsterMidGroupsExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint group_id;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] battle_group_vec;
}