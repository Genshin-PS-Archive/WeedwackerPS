using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class NewActivityWatcherConfig : WatcherConfig
{
	[ColumnIndex(0)]
	public uint? rewardID;
	public uint rewardPreview;
	public uint activitychallengetipsTextMapHash;
	public uint extraActivitychallengetipsTextMapHash;
	[ColumnIndex(1)]
	public bool isAutoGrant;
}