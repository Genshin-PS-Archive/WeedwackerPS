using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class ExhibitionScoreExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(2)]
	public uint seriesID;
	[ColumnIndex(4)]
	public ExhibitionScoreType scoreType;
	[ColumnIndex(5)]
	public uint score;
	public uint descTextMapHash;
	public bool showOut;

	//not in client
	[ColumnIndex(1)]
	public uint DisplayID;
	[ColumnIndex(3)]
	public uint? DisplayParam;
}