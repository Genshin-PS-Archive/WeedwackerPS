using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class ExhibitionListExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public bool defaultShow;
	[ColumnIndex(1)]
	public uint displayId;
	[ColumnIndex(2)]
	public uint seriesID;
	public uint displayTitleTextMapHash;
	public uint displayFormatTextMapHash;
	[ColumnIndex(3)]
	public ExhibitionListDisplayType displayType;
}