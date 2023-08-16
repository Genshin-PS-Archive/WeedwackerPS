namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class AvatarCodexData
{
	[ColumnIndex(0)]
	public uint sortId;
	[ColumnIndex(1)]
	public int sortFactor;
	[ColumnIndex(2)]
	public uint avatarId;
	[ColumnIndex(3)]
	public string beginTime;
	[ColumnIndex(4)]
	public bool hideWhenDontHave;
}