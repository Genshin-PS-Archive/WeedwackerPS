namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class QuestResCollectionExcelConfig
{
    [ColumnIndex(0)]
    public uint id;
    [ColumnIndex(2)]
    public bool isForbidDelete;

	//not in client
	[ColumnIndex(1)]
	public string name;
}