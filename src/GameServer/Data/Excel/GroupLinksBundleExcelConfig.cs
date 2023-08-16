using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class GroupLinksBundleExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public ActivityGroupLinkType linkType;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] groupList;
	[ColumnIndex(3)]
	public uint hintGroup;
	[ColumnIndex(4)]
	public uint? hintRadius;
	[ColumnIndex(5)]
	public ActivityGroupLinkRewardType? rewardType;
	[ColumnIndex(6)]
	public uint? rewardId; //GroupLinksBundleRewardExcelConfig
	[ColumnIndex(7)]
	public uint? reviseLevel;
	public string icon;
	public uint nameTextMapHash;
	public uint tipsTextMapHash;
	public ActivityGroupLinkPlayType playType;
	[ColumnIndex(9)]
	public bool autoTracingAfterActive;
	public uint uiRadius;
	public uint trackId;
	public bool guestShow;
	[ColumnIndex(10)]
	public bool isDefaultShowMark;

	//not in client
	[ColumnIndex(8)]
	public bool TracingActiveByDefault;
	[ColumnIndex(11)]
	public bool BigMapShowYellowCircle;
	[ColumnIndex(12)]
	public bool AlwaysShowYellowCircle;
}