using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class HideAndSeekSkillExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint? challengeID;
	[ColumnIndex(2)]
	public uint skillID;
	public uint order;
	[ColumnIndex(3)]
	public HIDE_AND_SEEK_SKILL_CATEGORY skillCategory;
	[ColumnIndex(4)]
	public HIDE_AND_SEEK_SKILL_SUB_CATEGORY skillSubCategory;
	[ColumnIndex(5)]
	public bool isDefault;
	public uint categoryDescTextMapHash;
	public byte skillIconPathHashPre;
	public uint skillIconPathHashSuffix;

	//not in client
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] GalleryAbilityGroupsList;
}