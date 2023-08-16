namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class GivingGroupExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] ItemIds;
	[ColumnIndex(2)]
	public uint? finishTalkId;
	[ColumnIndex(3)]
	public uint? mistakeTalkId;
	[ColumnIndex(4)]
	public uint? finishDialogId;
}