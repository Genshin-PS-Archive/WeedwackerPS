namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class GravenInnocenceExcelConfig
{
	[ColumnIndex(0)]
	public uint activityID;
	[ColumnIndex(1)]
	public uint successfullPhotoReminderID;
	[ColumnIndex(2)]
	public uint failPhotoReminderID;
	[ColumnIndex(3)]
	public string takePictureLuaConfigVar;
}
