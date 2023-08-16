namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class HuntingClueGatherExcelConfig
{
	[ColumnIndex(0)]
	public uint configId;
	[ColumnIndex(1)]
	public uint gatherId;
	[ColumnIndex(2)]
	public uint gatherGroupId;
	[ColumnIndex(3)]
	public bool isClueGather;
}