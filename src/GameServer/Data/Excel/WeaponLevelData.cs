namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class WeaponLevelData //: ItemConfig
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)][TsvArray(5)]
	public uint?[] requiredExps;
}
