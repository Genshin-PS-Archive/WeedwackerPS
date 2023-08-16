using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class GlobalWatcherConfig : WatcherConfig
{
	[ColumnIndex(0)][TsvArray(2)]
	public WatcherPredicateConfig[] predicateConfigs;

	[Columns(2)]
	public class WatcherPredicateConfig
	{
		[ColumnIndex(0)]
		public WatcherPredicateType? predicateType;
		[ColumnIndex(1)][TsvArray(2)]
		public uint?[] paramList;
	}
}