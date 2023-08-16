namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class AvatarPointExcelConfig
{
	[ColumnIndex(0)]
	public uint Type;
	[ColumnIndex(1)]
	public uint Star;
	[ColumnIndex(2)]
	public uint Level;
	[ColumnIndex(3)]
	public uint Points;
}
