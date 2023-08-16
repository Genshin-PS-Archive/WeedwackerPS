namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class DragonSpineStageExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint chapterId;
	public uint descTextMapHash;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] coin_id_list;
	[ColumnIndex(3)]
	public uint openday;
	[ColumnIndex(4)]
	public uint preQuestId;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] mission_id_list;
	[ColumnIndex(6)]
	public uint? rewardPreviewId;
}