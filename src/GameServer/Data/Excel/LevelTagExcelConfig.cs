namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class LevelTagExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public string levelTagName;
	[ColumnIndex(2)]
	public uint sceneId;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] addSceneTagIdList;
	[ColumnIndex(4)][TsvArray(',')]
    public uint[] removeSceneTagIdList;
	public bool levelTagCanFixTime;
	[ColumnIndex(5)]
	public float levelTagFixedEnviroTime;
	[ColumnIndex(6)][TsvArray(',')]
    public uint[] loadDynamicGroupList;
}