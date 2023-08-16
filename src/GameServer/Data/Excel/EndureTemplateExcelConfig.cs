using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class EndureTemplateExcelConfig
{
	[ColumnIndex(0)]
	public EndureType type;
	[ColumnIndex(1)]
	public float gaugeLength;
	[ColumnIndex(2)]
	public float waneSpeed;
	[ColumnIndex(3)]
	public float recoverTime;
	[ColumnIndex(4)]
	public float endurance;
}