namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class AvatarFlycloakData
{
	[ColumnIndex(0)]
	public uint flycloakId;
	public ulong nameTextMapHash;
	public uint descTextMapHash;
	public string prefab_path;
	public string json_name;
	public string icon;
	[ColumnIndex(1)]
	public uint materialId;
	public bool hide;
}
