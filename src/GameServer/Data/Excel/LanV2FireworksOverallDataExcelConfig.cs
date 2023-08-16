namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class LanV2FireworksOverallDataExcelConfig
{
	[ColumnIndex(0)]
	public uint scheduleId;
	[ColumnIndex(1)]
	public uint initStaminaValue;
	[ColumnIndex(2)][TsvArray(3)]
	public FireElementAdditionConfig[] fireElementAdditionList;
	[ColumnIndex(8)]
	public float scoreFormulaExponent;
	[ColumnIndex(9)]
	public float scoreFormulaOffset;
	public uint pushTipsId;

	[Columns(2)]
	public class FireElementAdditionConfig
	{
		[ColumnIndex(0)][TsvArray(',')]
		public uint[] range;
		[ColumnIndex(1)]
		public uint ratio;
	}
}