namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(3)]
public class InferencePageExcelConfig
{
	[ColumnIndex(0)]
	public uint PageID;
	[ColumnIndex(1)]
	public uint ParentTaskID;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] GroupConclusionList;
}
