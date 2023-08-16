namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class NpcFirstMetExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] subQuestIdList;
	[ColumnIndex(2)]
	public uint avatarID;
	public uint avatarDescriptionTextMapHash;
}