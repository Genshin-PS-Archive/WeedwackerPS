namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class RogueDiaryCardWeightExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint srCount;
	[ColumnIndex(2)]
	public uint weight;
	[ColumnIndex(3)]
	public uint retryWeight;
}