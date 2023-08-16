namespace Weedwacker.GameServer.Data.Excel;

[Columns(14)]
public class FishBaitExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(4)]
	public FishBaitFeature[] featureList;
	public byte sort;
	[ColumnIndex(13)][TsvArray(',')]
	public uint[] poolIdList;

	[Columns(3)]
	public class FishBaitFeature
	{
		[ColumnIndex(0)]
		public uint? featureTag;
		[ColumnIndex(1)]
		public float? weight;
		[ColumnIndex(2)]
		public float? bonusRange;
	}
}