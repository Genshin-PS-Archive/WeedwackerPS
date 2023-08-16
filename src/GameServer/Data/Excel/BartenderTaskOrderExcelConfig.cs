namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class BartenderTaskOrderExcelConfig
{
	[ColumnIndex(0)]
	public uint questId; //child quest
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] orderId;
	[ColumnIndex(2)]
	public uint contentParam;
	public uint dialogIdSucc;
	public uint dialogIdFail;
	public bool isGuideQuest;
	public uint nextQuestId;
}