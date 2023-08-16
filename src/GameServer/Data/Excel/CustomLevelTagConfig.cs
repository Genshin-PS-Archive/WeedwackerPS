namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class CustomLevelTagConfig
{
	[ColumnIndex(0)]
	public uint configId;
	public uint tagTitleTextMapHash;
	public uint sortId;
	[ColumnIndex(1)]
	public bool isDefault;
}