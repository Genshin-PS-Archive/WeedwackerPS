using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(18)]
public class GatherData
{
	[ColumnIndex(1)]
	public uint id;
	[ColumnIndex(2)]
	public uint? area_id;
	[ColumnIndex(0)]
	public uint pointType;
	[ColumnIndex(3)]
	public uint gadgetId;
	[ColumnIndex(4)]
	public uint itemId;
	[ColumnIndex(5)][TsvArray(',')] //no actual arrays
	public uint[] extraItemIdVec;
	[ColumnIndex(6)]
	public PointLocation pointLocation;
	[ColumnIndex(7)]
	public uint cd;
	[ColumnIndex(8)]
	public uint priority;
	[ColumnIndex(9)]
	public uint? refreshId;
	[ColumnIndex(10)][TsvArray(1)]
	public BlockLimit[] blockLimits;
	[ColumnIndex(12)]
	public bool init_disable_interact;
	[ColumnIndex(13)]
	public bool isForbidGuest;
	[ColumnIndex(14)]
	public GatherSaveType? saveType;
	[ColumnIndex(15)]
	public bool isForbidMpMode;
	//new
	[ColumnIndex(16)]
	public bool isPolymorphicCollection;
	[ColumnIndex(17)]
	public bool isscannablebycnuy;

	[Columns(2)]
	public class BlockLimit
	{
		[ColumnIndex(0)]
		public uint blockId;
		[ColumnIndex(1)]
		public uint count;
	}
}
