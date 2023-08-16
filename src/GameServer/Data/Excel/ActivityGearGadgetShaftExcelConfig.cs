namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class ActivityGearGadgetShaftExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint gadgetID;
	[ColumnIndex(2)]
	public float radius;
	[ColumnIndex(3)][TsvArray(',')]
	public float[] layerOffsetList;
	[ColumnIndex(4)]
	public float canNotPlaceTipOffset;
	[ColumnIndex(5)][TsvArray(',')]
	public float[] clenchRate;
}