namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class EchoShellFloatSignalExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint groupId;
	[ColumnIndex(2)]
	public uint configId;
}