using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(18)]
public class EquipAffixData
{
	[ColumnIndex(0)]
	public uint affixId;
	[ColumnIndex(16)]
	public uint id;
	[ColumnIndex(17)]
	public uint level;
	public uint nameTextMapHash;
	public uint descTextMapHash;
	[ColumnIndex(1)]
	public string openConfig;
	[ColumnIndex(2)][TsvArray(3)]
	public PropValConfig[] addProps;
	[ColumnIndex(8)][TsvArray(8)]
	public float?[] paramList;
}
