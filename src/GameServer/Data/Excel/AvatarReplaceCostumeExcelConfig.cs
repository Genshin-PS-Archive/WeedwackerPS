namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class AvatarReplaceCostumeExcelConfig
{
	[ColumnIndex(0)]
	public uint avatarId;
	[ColumnIndex(1)]
	public uint replace_costume_id;
}