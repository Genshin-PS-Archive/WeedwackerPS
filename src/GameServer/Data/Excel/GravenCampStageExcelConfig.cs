namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(3)]
public class GravenCampStageExcelConfig
{
	[ColumnIndex(0)]
	public uint StageID;
	[ColumnIndex(1)]
	public uint openDays;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] ChallengeCampIDList;
}
