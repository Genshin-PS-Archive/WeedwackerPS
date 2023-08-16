namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class BlossomTalkExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint refreshId;
	[ColumnIndex(2)]
	public uint groupId;
	[ColumnIndex(3)][TsvArray(';')]
	public uint[] talkId;
}