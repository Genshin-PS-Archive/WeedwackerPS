using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(5)]
public class WatcherTriggerConfig
{
	[ColumnIndex(0)]
	public WatcherTriggerType triggerType;
	[ColumnIndex(1)][TsvArray(4)]
	public string?[] paramList;
}