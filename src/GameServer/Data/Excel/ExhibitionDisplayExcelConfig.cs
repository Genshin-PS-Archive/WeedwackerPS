using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

//not in toilet dump ;_;
[Columns(11)]
public class ExhibitionDisplayExcelConfig
{
	[ColumnIndex(0)]
	public uint DisplayID;
	[ColumnIndex(1)]
	public uint SeriesID;
	[ColumnIndex(2)]
	public ExhibitionDisplayCondType condType;
	[ColumnIndex(3)][TsvArray(4)]
	public ExhibitionDisplayCondParam[] parameters;

	//this is in the toilet
	[Columns(2)]
	public class ExhibitionDisplayCondParam
	{
		[ColumnIndex(0)]
		public ExhibitionDisplayCondParamType? paramType;
		[ColumnIndex(1)]
		public string? param;
	}
}
