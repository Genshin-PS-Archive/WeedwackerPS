using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(21)]
public class GadgetData
{
	[ColumnIndex(7)]
	public EntityType? type;
	[ColumnIndex(9)]
	public string jsonName;
	[ColumnIndex(10)]
	public bool hasMove;
	[ColumnIndex(11)]
	public bool hasAudio;
	[ColumnIndex(12)]
	public bool isEquip;
	[ColumnIndex(13)]
	public bool isInteractive;
	[ColumnIndex(14)]
	public VisionLevelType? visionLevel;
	public string[] tags;
	public byte clientScriptHashPre;
	public uint clientScriptHashSuffix;
	[ColumnIndex(16)]
	public string itemJsonName;
	public byte itemPrefabPathHashPre;
	public uint itemPrefabPathHashSuffix;
	public uint radarHintID;
	public string inteeIconName;
	public uint landSoundID;
	[ColumnIndex(17)]
	public uint? mpPropID;
	public uint interactNameTextMapHash;
	[ColumnIndex(18)]
	public uint? chainId;
	[ColumnIndex(20)]
	public bool hasDynamicBarrier;
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

	//not in client
	[ColumnIndex(8)]
	public string description;
	[ColumnIndex(15)]
	public string serverScript;
	[ColumnIndex(19)]
	public bool canEdit;
}
