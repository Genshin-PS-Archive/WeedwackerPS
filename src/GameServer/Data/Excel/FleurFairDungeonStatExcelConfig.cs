using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class FleurFairDungeonStatExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint? galleryId;
	[ColumnIndex(2)]
	public FleurFairDungeonStatType statType;
	[ColumnIndex(3)][TsvArray(2)]
	public string[] statParamList;
	[ColumnIndex(5)]
	public OrderingType orderingType;
	[ColumnIndex(6)]
	public FleurFairDungeonStatMethod? statMethod;
	[ColumnIndex(7)]
	public uint priority;
	public uint titleTextMapHash;
	public uint descTextMapHash;
}