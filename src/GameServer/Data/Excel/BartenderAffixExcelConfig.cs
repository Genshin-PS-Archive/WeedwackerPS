using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class BartenderAffixExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public OrderingType orderType;
	[ColumnIndex(2)]
	public uint materialId;
	[ColumnIndex(3)]
	public uint materialCount;
}