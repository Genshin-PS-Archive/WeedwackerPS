namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class ActivityGearLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] providedGears;
	[ColumnIndex(2)]
	public uint openDayIndex;
	public uint levelNameTextMapHash;
	[ColumnIndex(3)]
	public uint watcherID;
	[ColumnIndex(4)]
	public uint redpointIDSubPage;
	[ColumnIndex(5)]
	public uint redpointIDLevelSelect;
}