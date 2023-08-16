namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class CusmtomGadgetConfigIdExcelConfig
{
	[ColumnIndex(0)]
	public uint configId;
	[ColumnIndex(1)]
	public uint itemId;
	[ColumnIndex(2)]
	public uint gadgetId;
	[ColumnIndex(3)]
	public string optionNameText;
	//public uint optionNameTextMapHash;
	[ColumnIndex(4)]
	public string optionTitleText;
	//public uint optionTitleTextMapHash;
}