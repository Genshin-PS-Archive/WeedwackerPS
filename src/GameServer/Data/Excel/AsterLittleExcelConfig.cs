namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class AsterLittleExcelConfig
{
	[ColumnIndex(0)]
	public uint stage_id;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[]? next_stage_id_vec;
	[ColumnIndex(2)]
	public uint open_day;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] mission_vec;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] watcher_id_vec;
	public uint titleTextMapHash;
	public uint descTextMapHash;
}