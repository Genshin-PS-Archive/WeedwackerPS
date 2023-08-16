namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(7)]
public class CharAmusementLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint stage_id;
	[ColumnIndex(1)]
	public uint random_weight;
	[ColumnIndex(2)]
	public uint stage_type;
	[ColumnIndex(3)]
	public uint galleryID;
	[ColumnIndex(4)]
	public uint singleplayer_target;
	[ColumnIndex(5)]
	public uint multiplayer_target;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] trial_character_pool;
}
