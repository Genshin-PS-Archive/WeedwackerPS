namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class ActivityChessMapExcelConfig
{
	[ColumnIndex(0)]
	public uint chessMapID;
	[ColumnIndex(1)]
	public string mapNameText;
	//public uint mapNameTextMapHash;
	[ColumnIndex(2)]
	public string descText;
	//public uint descTextMapHash;
	[ColumnIndex(3)]
	public string unlockTipsText;
	//public uint unlockTipsTextMapHash;
	[ColumnIndex(4)]
	public string mapIconPath;
	[ColumnIndex(5)]
	public uint buildGearLimit;
	[ColumnIndex(6)]
	public uint dungeonID;
	[ColumnIndex(7)]
	public uint entryPointId;
	[ColumnIndex(8)]
	public uint unlockDay;
	[ColumnIndex(9)]
	public bool isTeachMap;
	[ColumnIndex(10)]
	public uint? prevMapID;
	public bool show;
	public uint[] entrancePointIDList;
	public uint[] exitPointIDList;
}