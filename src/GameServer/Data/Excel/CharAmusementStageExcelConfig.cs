namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class CharAmusementStageExcelConfig
{
	[ColumnIndex(0)]
	public uint activity_stage;
	[ColumnIndex(1)]
	public uint level_type;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] play_numbers;
	[ColumnIndex(3)]
	public uint openDay;
}
