using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(20)]
public class SceneData
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public SceneType type;
	[ColumnIndex(2)]
	public SceneSubType? subtype;
	[ColumnIndex(3)]
	public bool ignoreNavMesh;
	[ColumnIndex(5)]
	public NavmeshModeType? navmeshMode;
	public string scriptData;
	[ColumnIndex(10)]
	public string overrideDefaultProfile; //not sure...
	[ColumnIndex(6)]
	public string levelEntityConfig;
	[ColumnIndex(7)]
	public uint? entityAppearSorted;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] specifiedAvatarList;
	[ColumnIndex(9)]
	public uint? maxSpecifiedAvatarNum;
	public float sceneFixTime;
	public string comment;
	[ColumnIndex(11)]
	public uint? safePoint;
	[ColumnIndex(12)]
	public SceneMpType? mpType;
	[ColumnIndex(13)]
	public bool isAllowMapMarkPoint;
	[ColumnIndex(14)]
	public bool isDeleteMapMarkPoint;
	[ColumnIndex(15)][TsvArray(',')]
	public uint[] dungeonEntryPoint;

	//not in client
	[ColumnIndex(4)]
	public bool enableSpacialPathfinding;
	[ColumnIndex(16)]
	public string dungeonGadgetType;
	[ColumnIndex(17)]
	public bool discardBlockBin;
	[ColumnIndex(18)]
	public bool discardSceneBin;
	[ColumnIndex(19)]
	public uint? associatedSceneID;
}
