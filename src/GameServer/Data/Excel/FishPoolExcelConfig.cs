using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(16)]
public class FishPoolExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] stockList;
	[ColumnIndex(2)][TsvDictionary(':', ',')]
	public Dictionary<uint, uint> stockGuarantee;
	[ColumnIndex(3)][TsvArray(2)]
	public FishStockLimit[] stockLimitList;
	[ColumnIndex(9)]
	public uint maxNum;
	public uint poolNameTextMapHash;
	public uint poolDescTextMapHash;
	[ColumnIndex(10)]
	public string abilityGroup;
	[ColumnIndex(11)]
	public string teamAbilityGroup;
	[ColumnIndex(12)][TsvArray(';')]
	public uint[] dropIdList;
	[ColumnIndex(13)]
	public uint? dailyLimitNum;
	[ColumnIndex(14)][TsvArray(',')]
	public uint[] excludeFish;
	[ColumnIndex(15)]
	public uint? cityId;

	[Columns(3)]
	public class FishStockLimit
	{
		[ColumnIndex(0)]
		public FishStockType? stockType;
		[ColumnIndex(1)]
		public uint? minNum;
		[ColumnIndex(2)]
		public uint? maxNum;
	}
}