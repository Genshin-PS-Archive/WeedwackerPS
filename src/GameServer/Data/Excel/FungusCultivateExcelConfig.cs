namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(9)]
public class FungusCultivateExcelConfig
{
	[ColumnIndex(0)]
	public uint levelId;
	[ColumnIndex(1)]
	public uint cultivateMushroomId;
	[ColumnIndex(2)]
	public uint? customClearPromotionTaskId;
	[ColumnIndex(3)]
	public uint theoreticalMinSteps;
	[ColumnIndex(4)]
	public uint? rotationLimit;
	[ColumnIndex(5)]
	public uint? offsetLimit;
	[ColumnIndex(6)]
	public uint? copyLimit;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] presetTemplate1;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] presetTemplate2;
}
