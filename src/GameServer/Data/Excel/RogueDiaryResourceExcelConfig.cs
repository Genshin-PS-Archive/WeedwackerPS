using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class RogueDiaryResourceExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public RogueDiaryResourceType type;
	[ColumnIndex(2)]
	public uint value;
}