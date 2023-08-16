namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class ActivityGearGadgetGearExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint gadgetID;
	[ColumnIndex(2)][TsvArray(',')]
	public float[] gearRadius;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] gearToothCount;
	[ColumnIndex(4)]
	public uint materialID;
	[ColumnIndex(5)]
	public string gearCanNotPlaceEffect;
	[ColumnIndex(6)]
	public string gearPlaceEffect;
	[ColumnIndex(7)]
	public string coverUpEffect;
}