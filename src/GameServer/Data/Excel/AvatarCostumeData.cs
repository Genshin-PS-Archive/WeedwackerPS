namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class AvatarCostumeData
{
	[ColumnIndex(0)]
	public uint skinId;
	[ColumnIndex(1)]
	public uint indexID;
	public uint nameTextMapHash;
	public uint descTextMapHash;
	[ColumnIndex(2)]
	public uint? itemId;
	[ColumnIndex(3)]
	public uint? characterId;
	public string json_name;
	[ColumnIndex(4)]
	public string? prefab_path;
	//public byte prefab_pathHashPre;
	//public uint prefab_pathHashSuffix;
	[ColumnIndex(5)]
	public string? prefab_path_remote;
	//public byte prefab_remote_pathHashPre;
	//public uint prefab_remote_pathHashSuffix;
	public byte prefab_npc_pathHashPre;
	public uint prefab_npc_pathHashSuffix;
	public byte animatorConfigPathHashPre;
	public uint animatorConfigPathHashSuffix;
	public byte prefab_manekin_pathHashPre;
	public uint prefab_manekin_pathHashSuffix;
	[ColumnIndex(6)]
	public string? controller_path;
	//public byte controller_pathHashPre;
	//public uint controller_pathHashSuffix;
	[ColumnIndex(7)]
	public string? controller_remote_path;
	//public byte controller_remote_pathHashPre;
	//public uint controller_remote_pathHashSuffix;
	[ColumnIndex(8)]
	public bool? isDefault;
	[ColumnIndex(9)]
	public bool? isDefaultUnlock;
	[ColumnIndex(10)]
	public uint? quality;
	public bool hide;
	public string frontIconName;
	public string sideIconName;
	public byte imageNameHashPre;
	public uint imageNameHashSuffix;
	public bool domesticHideInArtPreview;
	public bool overseaHideInArtPreview;
}