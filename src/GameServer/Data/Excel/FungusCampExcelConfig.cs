namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class FungusCampExcelConfig
{
	[ColumnIndex(0)]
	public uint campId;
	[ColumnIndex(1)]
	public uint GroupLinkID;
	[ColumnIndex(2)]
	public uint? CustomClearPromotionTaskID;
	[ColumnIndex(3)]
	public uint? pre_campId;
}
