namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class FurnitureSuiteExcelConfig
{
	[ColumnIndex(0)]
	public uint suiteID;
	[ColumnIndex(1)]
	public string jsonName;
	[ColumnIndex(2)]
	public string suiteNameText;
	//public uint suiteNameTextMapHash;
	public uint suiteDescTextMapHash;
	[ColumnIndex(3)]
	public uint? drawingID;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] favoriteNpcExcelIdVec;
	[ColumnIndex(5)]
	public string transStr;
	[ColumnIndex(6)][TsvArray(2)]
	public uint?[] furnType;
	[ColumnIndex(8)]
	public string itemIcon;
	[ColumnIndex(9)]
	public string mapIcon;
	[ColumnIndex(10)]
	public float? interRatio;
}