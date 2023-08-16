using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class BartenderOrderExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint formulaId;
	[ColumnIndex(2)][TsvArray(2)]
	public uint?[] affixId;
	[ColumnIndex(4)]
	public BartenderCupType? cupType;
	[ColumnIndex(5)]
	public uint? time;
	[ColumnIndex(6)]
	public uint? score;
	public string iconName;
	public uint descTextMapHash;
}