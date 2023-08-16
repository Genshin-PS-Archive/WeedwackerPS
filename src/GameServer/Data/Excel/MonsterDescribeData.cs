namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
internal class MonsterDescribeData
{
	[ColumnIndex(0)]
	public uint id;
	public uint nameTextMapHash;
	[ColumnIndex(1)]
	public uint titleId;
	[ColumnIndex(2)]
	public uint specialNameLabID;
	public string icon;
}
