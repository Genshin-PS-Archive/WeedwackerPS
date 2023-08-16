using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(32)]
public class GadgetInteractExcelConfig
{
	[ColumnIndex(0)]
	public uint interactId;
	[ColumnIndex(1)]
	public InteractActionType? actionType;
	[ColumnIndex(2)]
	public uint? param1;
	[ColumnIndex(3)]
	public uint? param2;
	[ColumnIndex(4)][TsvArray(2)]
	public GadgetInteractActionConfig[] actionList;
	[ColumnIndex(10)]
	public bool isGuestInteract;
	[ColumnIndex(12)][TsvArray(6)]
	public IdCountConfig[] costItems;
	public uint uiTitleTextMapHash;
	public uint uiDescTextMapHash;
	[ColumnIndex(24)][TsvArray(2)]
	public GadgetInteractCond[] condList;
	[ColumnIndex(30)]
	public LogicType? condComb;
	[ColumnIndex(31)]
	public bool isMpModeInteract;

	[Columns(3)]
	public class GadgetInteractActionConfig
	{
		[ColumnIndex(0)]
		public InteractActionType? actionType;
		[ColumnIndex(1)][TsvArray(2)]
		public uint?[] param;
	}
	[Columns(3)]
	public class GadgetInteractCond
	{
		[ColumnIndex(0)]
		public InteractCondType? condType;
		[ColumnIndex(1)][TsvArray(2)]
		public string?[] param;
	}

	//not in client
	[ColumnIndex(11)]
	public string remark;
}