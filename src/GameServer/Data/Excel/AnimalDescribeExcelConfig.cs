namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class AnimalDescribeExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint nameTextMapHash;
	public string icon;
}