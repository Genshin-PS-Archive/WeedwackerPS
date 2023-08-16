namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(8)]
public class WatcherConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public WatcherTriggerConfig triggerConfig;
	[ColumnIndex(6)]
	public uint? progress;
	[ColumnIndex(7)]
	public bool isDisuse;
}