namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class InstableSprayDifficultyExcelConfig
{
	[ColumnIndex(0)]
	public uint DifficultyLevel;
	[ColumnIndex(1)]
	public string DifficultyDescriptionText;
	[ColumnIndex(2)]
	public uint IntegralMultiplier;
	[ColumnIndex(3)]
	public uint InitialDungeonLevel;
}
