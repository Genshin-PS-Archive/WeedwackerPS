namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class PlayerLevelExcelConfig
{
	public uint maxExp;
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)]
	public uint exp;
	[ColumnIndex(2)]
	public uint? rewardId;
	[ColumnIndex(3)]
	public uint? expeditionLimitAdd;
	[ColumnIndex(4)]
	public uint? unlockWorldLevel;
	public uint unlockDescTextMapHash;
}