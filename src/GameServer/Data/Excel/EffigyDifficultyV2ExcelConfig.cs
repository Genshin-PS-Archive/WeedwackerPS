namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(3)]
public class EffigyDifficultyV2ExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint? reviseLevel;
	[ColumnIndex(2)]
	public uint activeSkills;
}
