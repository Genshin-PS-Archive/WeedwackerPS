namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class TriggerExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint sceneId;
	[ColumnIndex(2)]
	public uint groupId;
	[ColumnIndex(3)]
	public string triggerName;
}