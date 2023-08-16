namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class AvatarFetterLevelData
{
	[ColumnIndex(0)]
	public uint fetterLevel;
	[ColumnIndex(1)]
	public uint needExp;
}
