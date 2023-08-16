namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(2)]
public class InferenceConclusionExcelConfig
{
	[ColumnIndex(0)]
	public uint GroupConclusionID;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] EntryIDList;
}
