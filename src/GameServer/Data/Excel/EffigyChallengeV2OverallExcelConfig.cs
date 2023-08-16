namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class EffigyChallengeV2OverallExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint schedule_id;
	[ColumnIndex(2)]
	public uint challengeDifficultyLevel;
	[ColumnIndex(3)]
	public string SGVName;
}
