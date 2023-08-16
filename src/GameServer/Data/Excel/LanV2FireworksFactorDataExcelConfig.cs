using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class LanV2FireworksFactorDataExcelConfig
{
	[ColumnIndex(0)]
	public uint factorId;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] perfectRange;
	[ColumnIndex(2)]
	public uint factorLength;
	[ColumnIndex(3)]
	public FireworksReformParamType type;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] colorRange;
}