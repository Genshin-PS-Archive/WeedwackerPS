namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class IrodoriChessGearExcelConfig
{
	[ColumnIndex(0)]
	public uint gearId;
	[ColumnIndex(3)]
	public uint gadgetId;
	[ColumnIndex(4)]
	public bool? isEnableRotate;
	[ColumnIndex(5)]
	public uint initLevel;
	public uint gearNameTextMapHash;
	public uint gearShortNameTextMapHash;
	public uint descTextMapHash;
	public string gearIconPath;
	public string tagIconPath;
	public string mapIconPath;
	public int attack;
	public int mastery;
	public int attackSpeed;
	public int attackRange;

	//not in client
	[ColumnIndex(1)]
	public uint buildCost;
	[ColumnIndex(2)]
	public uint rebuildCost;
}