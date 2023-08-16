namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class DungeonLevelEntityConfig
{
	public uint clientId;
	[ColumnIndex(0)]
	public uint id;
	public bool show;
	[ColumnIndex(1)]
	public string levelConfigName;
	public uint descTextMapHash;
	public uint switchTitleTextMapHash;
}