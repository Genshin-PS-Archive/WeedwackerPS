namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class EchoShellRewardExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint rewardId;
	[ColumnIndex(2)]
	public uint shellCount;
	public bool showAtTop;
}