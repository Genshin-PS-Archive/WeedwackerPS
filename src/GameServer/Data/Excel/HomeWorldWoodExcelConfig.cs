namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(8)]
public class HomeWorldWoodExcelConfig
{
	[ColumnIndex(0)]
	public uint WoodID;
	[ColumnIndex(1)]
	public uint QuantityLimit;
	[ColumnIndex(2)]
	public uint RefreshType;
	[ColumnIndex(3)][TsvArray(';')]
	public uint[] ExchangeMaterial1;
	[ColumnIndex(4)][TsvArray(';')]
	public uint[] ExchangeMaterial2;
	[ColumnIndex(5)][TsvArray(';')]
	public uint[] ExchangeMaterial3;
	[ColumnIndex(6)][TsvArray(';')]
	public uint[] ExchangeMaterial4;
	[ColumnIndex(7)][TsvArray(';')]
	public uint[] ExchangeMaterial5;
}
