namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class IrodoriChessMapExcelConfig
{
	[ColumnIndex(0)]
	public uint mapId;
	[ColumnIndex(1)]
	public uint dungeonId;
	[ColumnIndex(2)]
	public uint entryPointId;
	public bool show;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] cardPool;
	[ColumnIndex(4)]
	public uint totalCardPoints;
	[ColumnIndex(5)][TsvDictionary(':', ',')]
	public Dictionary<uint, uint> singleGearLimits;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[] entrancePointList;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] exitPointList;
	[ColumnIndex(9)][TsvArray(',')]
	public uint[] disorderList;
	public uint[] entryPageDisorderList;
	[ColumnIndex(10)]
	public uint initBuildingPoints;
	public bool isHardMap;
	public uint mapNameTextMapHash;
	public uint descTextMapHash;
	public uint difficulty;
	public string mapIconPath;
	public Dictionary<uint, uint> presetGearMap;

	//not in client
	[ColumnIndex(6)]
	public uint PointMax; //not sure
}