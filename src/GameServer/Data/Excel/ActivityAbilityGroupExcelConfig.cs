namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class ActivityAbilityGroupExcelConfig
{
	[ColumnIndex(0)]
	public uint index;
	[ColumnIndex(1)]
	public uint activityId;
	[ColumnIndex(3)]
	public uint? avatarId;
	[ColumnIndex(4)]
	public uint? weaponId;
	public uint nameTextMapHash;

	//not in client
	[ColumnIndex(2)]
	public string abilityGroup;
}