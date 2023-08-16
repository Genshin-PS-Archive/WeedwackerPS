using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class CustomGadgetSlotLevelTagConfig
{
	[ColumnIndex(0)]
	public uint ID;
	public CustomGadgetRootType customGadgetType;
	[ColumnIndex(1)][TsvArray(5)]
	public CustomGadgetSlotGroup[] slotMap;
	[ColumnIndex(11)]
	public uint levelTagID;
	[ColumnIndex(12)]
	public uint GadgetId;

	[Columns(2)]
	public class CustomGadgetSlotGroup
	{
		[ColumnIndex(0)]
		public string slotIdentifier;
		[ColumnIndex(1)][TsvArray(1)]
		public uint?[] slotList;
	}
}