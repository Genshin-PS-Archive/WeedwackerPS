namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class BlossomGroupsExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)]
	public uint sectionId;
	[ColumnIndex(3)][TsvArray(';')]
	public uint[] refreshTypeVec;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] newGroupVec;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] decorateGroupVec;
	[ColumnIndex(6)][TsvArray(';')]
	public uint[] nextCampIdVec;
	[ColumnIndex(7)]
	public bool is_safe;
	[ColumnIndex(8)]
	public bool is_initial_refresh;
	[ColumnIndex(9)]
	public uint finish_progress;
	[ColumnIndex(10)]
	public uint limit_level;
	public uint fight_radius;
	public uint remind_radius;
	public uint blossom_tipsTextMapHash;
	[ColumnIndex(11)]
	public uint? delay_unload_sec;
}