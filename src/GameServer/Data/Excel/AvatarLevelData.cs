namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class AvatarLevelData
{
	[ColumnIndex(0)]
	public uint Level;
	[ColumnIndex(1)]
	public uint Exp;
	[ColumnIndex(2)]
	public uint? smallTalentPoint;
}
