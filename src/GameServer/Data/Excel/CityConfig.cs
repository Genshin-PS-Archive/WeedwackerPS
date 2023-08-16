using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class CityConfig
{
	[ColumnIndex(0)]
	public uint cityId;
	[ColumnIndex(1)]
	public uint sceneId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] areaIdVec; //areaID1
	[ColumnIndex(3)]
	public string cityName;
	//public uint cityNameTextMapHash;
	public int mapPosX;
	public int mapPosY;
	public float zoomForExploration;
	public uint adventurePointId;
	public string ExpeditionMap;
	public string ExpeditionWaterMark;
	[ColumnIndex(4)]
	public OpenStateType openState;
	[ColumnIndex(5)]
	public string cityGoddnessName;
	[ColumnIndex(6)]
	public string cityGoddnessDesc;
	//public uint cityGoddnessNameTextMapHash;
	//public uint cityGoddnessDescTextMapHash;
}