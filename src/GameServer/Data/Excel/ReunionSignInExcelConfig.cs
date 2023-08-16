namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class ReunionSignInExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint dayIndex;
	[ColumnIndex(2)][TsvArray(1)]
	public uint[] rewardId;
}