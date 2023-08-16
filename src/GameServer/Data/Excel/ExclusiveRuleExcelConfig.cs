using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

//not in toilet dump ;_;
[Columns(4)]
public class ExclusiveRuleExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public ExclusivePlatformType exclusivePlatform;
	[ColumnIndex(2)]
	public ExclusiveRuleType exclusiveRule;
	[ColumnIndex(3)]
	public uint itemID;
}
