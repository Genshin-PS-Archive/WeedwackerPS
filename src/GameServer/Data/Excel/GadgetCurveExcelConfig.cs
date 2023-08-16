using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(25)]
public class GadgetCurveExcelConfig
{
	[ColumnIndex(0)]
	public uint level;
	[ColumnIndex(1)][TsvArray(8)]
	public GrowCurveInfo[] curveInfos;
}