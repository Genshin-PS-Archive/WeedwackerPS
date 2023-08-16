namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(5)]
public class ExhibitionLuaStorageExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public string DataTriggerType;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] SceneIDGroup;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] GroupIDGroup;
	[ColumnIndex(4)]
	public uint? GroupTagGroup;
}
