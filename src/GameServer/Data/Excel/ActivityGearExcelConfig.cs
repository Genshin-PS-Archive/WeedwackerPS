namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class ActivityGearExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] gearLevelIdList;
	[ColumnIndex(3)]
	public float gearToothLength;
	[ColumnIndex(4)]
	public float gearThickness;
	[ColumnIndex(5)]
	public string shaftSelectEffect;
	[ColumnIndex(6)]
	public string shaftTipEffect;
	[ColumnIndex(7)]
	public string endGearSuccessEffect;
	[ColumnIndex(8)]
	public string coverUpEffect;
	[ColumnIndex(9)]
	public string gearPlaceEffect;
	[ColumnIndex(10)]
	public uint pushTipsId;
	[ColumnIndex(11)][TsvArray(',')]
	public uint[] watcherIdList;
	[ColumnIndex(12)]
	public float playerTipDelay;
}