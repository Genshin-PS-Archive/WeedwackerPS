namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class CoopInteractionExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(2)]
	public uint npcId;
	[ColumnIndex(3)]
	public uint mainQuestId;
	[ColumnIndex(4)]
	public uint priority;
	[ColumnIndex(5)]
	public bool isAuto;

	//not in client
	[ColumnIndex(1)]
	public string remarks;
}