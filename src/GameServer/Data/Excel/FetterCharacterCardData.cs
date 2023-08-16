namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class FetterCharacterCardData
{
	[ColumnIndex(0)]
	public uint avatarId;
	[ColumnIndex(1)]
	public uint fetterLevel;
	[ColumnIndex(2)]
	public uint rewardId;
}
