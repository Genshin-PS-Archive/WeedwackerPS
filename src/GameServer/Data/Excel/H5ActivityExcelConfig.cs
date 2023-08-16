using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class H5ActivityExcelConfig
{
	[ColumnIndex(0)]
	public uint h5ActivityId;
	public uint nameTextMapHash;
	public uint detailTextMapHash;
	public uint rewardPreview;
	public LogicType condComb;
	[ColumnIndex(1)][TsvArray(2)]
	public H5ActivityCondConfig[] condList;
}