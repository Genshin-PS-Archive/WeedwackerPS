using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class OverflowTransformExcelConfig
{
	public OverflowTransformType transformType;
	public uint transformId;
	public uint transformBaseCount;
	public IdCountConfig[] transformResults;
	public ItemLimitType transformItemLimitType;
}