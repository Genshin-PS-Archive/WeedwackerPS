namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class HomeWorldBgmExcelConfig
{
	[ColumnIndex(0)]
	public uint bgmID;
	[ColumnIndex(1)]
	public bool isDefaultUnlock;
	[ColumnIndex(2)]
	public bool isRoomSceneUsable;
	[ColumnIndex(3)]
	public bool isWorldSceneUsable;
	public uint cityId;
	public uint sortOrder;
	public string bgmPath;
	public uint bgmNameTextMapHash;
}