namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class WeaponCodexExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public uint weaponId;
	[ColumnIndex(2)]
	public uint SortOrder;
	[ColumnIndex(3)]
	public bool isDisuse;
	[ColumnIndex(4)]
	public bool showOnlyUnlocked;
}