namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(7)]
public class GravenCampRaceExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint openDays;
	[ColumnIndex(2)]
	public uint GalleryID;
	[ColumnIndex(3)]
	public uint GroupLinkID;
	[ColumnIndex(4)][TsvArray(';')]
	public uint[] IntegralWatcher;
	[ColumnIndex(5)]
	public uint IntegralTimeCoefficient;
	[ColumnIndex(6)]
	public uint IntegralGoldCoefficient;
}
