namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class RoomExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string name;
	public string prefabPath;
	public string navMeshPath;
}