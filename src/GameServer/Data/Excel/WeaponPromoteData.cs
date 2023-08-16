using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class WeaponPromoteData
{
	[ColumnIndex(0)]
	public uint weaponPromoteId;
	[ColumnIndex(1)]
	public uint promoteLevel;
	[ColumnIndex(2)][TsvArray(3)]
	public IdCountConfig[] costItems;
	[ColumnIndex(8)]
	public uint coinCost;
	[ColumnIndex(9)][TsvArray(5)]
	public PropValConfig[] addProps;
	[ColumnIndex(19)]
	public uint unlockMaxLevel;
	[ColumnIndex(20)]
	public uint requiredPlayerLevel;
}
