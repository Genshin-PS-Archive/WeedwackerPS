using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class WorldAreaConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint SceneID;
	[ColumnIndex(2)]
	public WorldAreaType AreaType;
	[ColumnIndex(3)]
	public uint AreaID_1;
	[ColumnIndex(4)]
	public uint? AreaID_2;
	[ColumnIndex(5)]
	public bool AreaDefaultLock;
	public uint AreaNameTextMapHash;
	[ColumnIndex(6)]
	public uint? tower_point_id;
	[ColumnIndex(7)]
	public ElementType? elementType;
	[ColumnIndex(8)]
	public AreaTerrainType? terrainType;
	[ColumnIndex(9)]
	public bool showTips;
	[ColumnIndex(10)]
	public float minimapScale;
}