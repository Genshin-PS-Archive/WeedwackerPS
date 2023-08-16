namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class CustomLevelDungeonConfig
{
	[ColumnIndex(0)]
	public uint dungeonID;
	public uint order;
	[ColumnIndex(1)]
	public string jsonPath;
	[ColumnIndex(2)]
	public string mapPrefabPath;
	public byte mapScenePicHashPre;
	public uint mapScenePicHashSuffix;
	public byte dataPicHashPre;
	public uint dataPicHashSuffix;
	[ColumnIndex(3)]
	public uint roomNum;
	public uint roomNameFormatTextMapHash;
	[ColumnIndex(4)]
	public string corridors;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] countBrickList;
}