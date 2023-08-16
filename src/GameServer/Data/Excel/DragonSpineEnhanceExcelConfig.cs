namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class DragonSpineEnhanceExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint? activityAbilityGroupId;
	[ColumnIndex(2)]
	public uint process;
	public uint descTextMapHash;
}