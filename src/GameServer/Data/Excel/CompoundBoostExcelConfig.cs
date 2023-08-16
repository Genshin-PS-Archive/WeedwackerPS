namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(2)]
public class CompoundBoostExcelConfig
{
	[ColumnIndex(0)]
	public uint materialID;
	[ColumnIndex(1)]
	public uint accelerationTimeInSeconds;
}
