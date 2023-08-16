
namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(7)]
public class EntityExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string nameText;
	//public uint nameTextMapHash;
	[ColumnIndex(2)]
	public string prefabPath;
	//public byte prefabPathHashPre;
	//public uint prefabPathHashSuffix;
	[ColumnIndex(3)]
	public string prefabPathRemote;
	//public byte prefabPathRemoteHashPre;
	//public uint prefabPathRemoteHashSuffix;
	[ColumnIndex(4)]
	public string controllerPath;
	//public byte controllerPathHashPre;
	//public uint controllerPathHashSuffix;
	[ColumnIndex(5)]
	public string controllerPathRemote;
	//public byte controllerPathRemoteHashPre;
	//public uint controllerPathRemoteHashSuffix;
	[ColumnIndex(6)]
	public uint? campID;
	public string LODPatternName;
}