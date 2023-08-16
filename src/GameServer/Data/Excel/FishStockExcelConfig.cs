using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class FishStockExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public FishStockType type;
	[ColumnIndex(2)][TsvDictionary(':', ',')]
	public Dictionary<uint, uint> fishWeight;
}