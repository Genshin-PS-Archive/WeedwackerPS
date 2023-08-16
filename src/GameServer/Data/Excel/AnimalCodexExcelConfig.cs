using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class AnimalCodexExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public AnimalCodexType type;
	[ColumnIndex(2)]
	public uint describeId;
	[ColumnIndex(3)]
	public bool isSeenActive;
	[ColumnIndex(4)]
	public uint SortOrder;
	[ColumnIndex(5)]
	public bool isDisuse;
	[ColumnIndex(6)]
	public bool showOnlyUnlocked;
	[ColumnIndex(7)]
	public AnimalCodexSubType subType;
	public AnimalCodexCountType countType;
	[ColumnIndex(8)]
	public string descText;
	//public uint descTextMapHash;
	public string modelPath;
	public uint pushTipsCodexId;
}