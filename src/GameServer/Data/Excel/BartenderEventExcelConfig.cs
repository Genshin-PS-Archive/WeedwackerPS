using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class BartenderEventExcelConfig
{
	[ColumnIndex(0)]
	public BartenderEffectType effectType;
	[ColumnIndex(1)]
	public uint? miscId;
	[ColumnIndex(2)]
	public string effectName;
}