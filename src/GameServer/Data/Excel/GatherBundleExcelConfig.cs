namespace Weedwacker.GameServer.Data.Excel;

[Columns(34)]
public class GatherBundleExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public string bundleName;
	[ColumnIndex(1)]
	public uint baseGadgetId;
	[ColumnIndex(2)][TsvArray(4)]
	public PointInfo[] points;

	[Columns(8)]
	public class PointInfo
	{
		[ColumnIndex(0)]
		public uint? pointID;
		[ColumnIndex(1)]
		public uint? pointType;
		[ColumnIndex(2)]
		public float? offsetX;
		[ColumnIndex(3)]
		public float? offsetY;
		[ColumnIndex(4)]
		public float? offsetZ;
		[ColumnIndex(5)]
		public float? rotX;
		[ColumnIndex(6)]
		public float? rotY;
		[ColumnIndex(7)]
		public float? rotZ;
	}
}