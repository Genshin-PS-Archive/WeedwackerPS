using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(15)]
public class MaterialCodexExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public MaterialCodexType type;
	[ColumnIndex(2)]
	public uint materialId;
	[ColumnIndex(3)]
	public uint SortOrder;
	[ColumnIndex(4)]
	public string nameText;
	//public uint nameTextMapHash;
	[ColumnIndex(5)]
	public string descText;
	//public uint descTextMapHash;
	[ColumnIndex(6)]
	public string icon;
	[ColumnIndex(7)]
	public bool isDisuse;
	[ColumnIndex(8)]
	public bool showOnlyUnlocked;

	//not in client---------//
	[ColumnIndex(9)][TsvArray(2)]
	public Quest[] quests;

	[Columns(3)]
	public class Quest
	{
		[ColumnIndex(0)]
		public string nameTextTitle1;
		[ColumnIndex(1)]
		public string descTextDesc1;
		[ColumnIndex(2)]
		public uint? questId;
	}
	//----------------------//
}