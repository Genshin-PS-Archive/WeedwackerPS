using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(19)]
public class GivingExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public uint? talkId;
	[ColumnIndex(2)]
	public uint? mistakeTalkId;
	public BagTab tab;
	[ColumnIndex(3)]
	public bool isRepeatable;
	[ColumnIndex(4)]
	public GivingMethod givingMethod;
	[ColumnIndex(5)][TsvArray(3)]
	public IdCountConfig[] exactItems;
	[ColumnIndex(11)]
	public uint? exactFinishTalkId;
	[ColumnIndex(12)][TsvArray(',')]
	public uint[] givingGroupIds;
	[ColumnIndex(13)]
	public uint? givingGroupCount;
	public bool highlight;
	public string icon;
	[ColumnIndex(14)]
	public bool isRemoveItem;
	[ColumnIndex(15)]
	public bool isReset;
	[ColumnIndex(16)]
	public bool isMpEnable;
	[ColumnIndex(17)]
	public GivingType givingType;
	[ColumnIndex(18)]
	public bool isTakeBack;
}