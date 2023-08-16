using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class RefreshPolicyExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public RefreshType type;
	[ColumnIndex(2)]
	public string time;
}