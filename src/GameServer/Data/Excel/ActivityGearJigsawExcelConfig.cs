namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class ActivityGearJigsawExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint mainQuestID;
	[ColumnIndex(2)]
	public string placePosSelectEffect;
	[ColumnIndex(3)]
	public string coverUpEffect;
	[ColumnIndex(4)]
	public string rotateEffect;
	[ColumnIndex(5)]
	public uint questStateTriggerId;
	[ColumnIndex(6)]
	public uint pushTipsId;
}