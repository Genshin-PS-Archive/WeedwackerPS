using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(4)]
public class ExhibitionSeriesExcelConfig
{
	[ColumnIndex(0)]
	public uint SeriesID;
	[ColumnIndex(1)]
	public ExhibitionSeriesType openType;
	[ColumnIndex(2)]
	public uint? openParam;
	[ColumnIndex(3)]
	public bool refreshEveryTime;
}
