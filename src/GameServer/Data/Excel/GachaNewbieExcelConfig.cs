namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(20)]
public class GachaNewbieExcelConfig
{
	[ColumnIndex(0)]
	public uint SinglePullItemID;
	[ColumnIndex(1)]
	public uint SinglePullItemCount;
	[ColumnIndex(2)]
	public uint TenPullItemID;
	[ColumnIndex(3)]
	public uint TenPullItemCount;
	[ColumnIndex(4)]
	public uint FirstTenPullItemID;
	[ColumnIndex(5)]
	public uint FirstTenPullItemCount;
	[ColumnIndex(6)]
	public uint MaxPulls;
	[ColumnIndex(7)]
	public uint GachaPoolID;
	[ColumnIndex(8)]
	public uint ProbabilityRuleID;
	[ColumnIndex(9)]
	public UPConfig UP;
	[ColumnIndex(12)][TsvArray(',')]
	public uint[] GuaranteeRuleList;
	[ColumnIndex(13)]
	public string GachaPrefabPath;
	[ColumnIndex(14)]
	public string GachaPreviewPrefabPath;
	[ColumnIndex(15)]
	public string GachaProbabilityAnnouncementURL;
	[ColumnIndex(16)]
	public string GachaRecordURL;
	[ColumnIndex(17)]
	public string GlobalGachaProbabilityAnnouncementURL;
	[ColumnIndex(18)]
	public string GlobalGachaRecordURL;
	[ColumnIndex(19)]
	public uint sortId;

	[Columns(3)]
	public class UPConfig
	{
		[ColumnIndex(0)]
		public uint? ParentType;
		[ColumnIndex(1)]
		public uint? Probability;
		[ColumnIndex(2)][TsvArray(',')]
		public uint[] ItemList;
	}
}
