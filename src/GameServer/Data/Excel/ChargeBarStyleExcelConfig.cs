using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class ChargeBarStyleExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public string iconName;
	public ChargeFillEffect fillEffectType;
}