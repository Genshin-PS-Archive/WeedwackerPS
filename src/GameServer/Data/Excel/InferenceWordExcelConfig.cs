namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(14)]
public class InferenceWordExcelConfig
{
	[ColumnIndex(0)]
	public uint EntryID;
	[ColumnIndex(1)]
	public uint PageID;
	[ColumnIndex(2)]
	public bool UnlockByDefault;
	[ColumnIndex(3)]
	public bool IsDecipherable;
	[ColumnIndex(4)][TsvArray(2)]
	public InferenceExecution[] InferenceExec;
	[ColumnIndex(8)]
	public bool CanAssociate;
	[ColumnIndex(9)]
	public uint? AssociationEntryID;
	[ColumnIndex(10)][TsvArray(2)]
	public InferenceExecution[] AssociationExec;

	[Columns(2)]
	public class InferenceExecution
	{
		[ColumnIndex(0)]
		public uint? type;
		[ColumnIndex(1)]
		public uint? parameter;
	}
}
