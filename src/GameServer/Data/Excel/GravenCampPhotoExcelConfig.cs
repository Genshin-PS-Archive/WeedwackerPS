namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class GravenCampPhotoExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint openDays;
	[ColumnIndex(2)][TsvArray(';')]
	public uint[] objectList;
	[ColumnIndex(3)]
	public uint shootingLimit;
}
