namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class ActivityChessGearExcelConfig
{
	[ColumnIndex(0)]
	public uint gearID;
	public uint gearNameTextMapHash;
	public uint gearShortNameTextMapHash;
	public uint descTextMapHash;
	public string gearIconPath;
	public string tagIconPath;
	public int attack;
	public int mastery;
	public int attackSpeed;
	public int attackRange;
	[ColumnIndex(1)]
	public uint build_cost;
	[ColumnIndex(2)]
	public uint demolition_refund;
	[ColumnIndex(3)]
	public uint gadget_id;
	[ColumnIndex(4)]
	public uint? build_limit;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] ban_dungeon_list;
	[ColumnIndex(6)]
	public bool isEnableRotate;
	[ColumnIndex(7)]
	public uint initLevel;
	[ColumnIndex(8)]
	public uint needChessLevel;
}