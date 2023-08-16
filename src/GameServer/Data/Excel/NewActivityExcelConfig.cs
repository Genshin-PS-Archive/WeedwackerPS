using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class NewActivityExcelConfig
{
	[ColumnIndex(0)]
	public uint activityId;
	[ColumnIndex(1)]
	public NewActivityType activityType;
	public uint nameTextMapHash;
	[ColumnIndex(7)]
	public string activitySceneTag; //not sure
	[ColumnIndex(2)]
	public bool isLoadTerrain;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] condGroupId;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] watcherId;
	[ColumnIndex(5)]
	public bool isBanClientUi;

	//not in client
	[ColumnIndex(6)]
	public bool clearActivityBin;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] associatedDungeonId;
}