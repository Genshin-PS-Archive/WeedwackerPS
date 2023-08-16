namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(5)]
public class GachaPoolExcelConfig
{
	[ColumnIndex(0)]
	public uint PoolID;
	[ColumnIndex(1)]
	public uint ItemID;
	[ColumnIndex(2)]
	public uint Type;
	[ColumnIndex(3)]
	public uint? ProbabilityWeight;
	[ColumnIndex(4)]
	public uint? FlashCardProbability;
}
