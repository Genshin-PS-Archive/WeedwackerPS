using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class AchievementExcelConfig : WatcherConfig
{	
	public uint goalId;
	[ColumnIndex(0)]
	public uint orderId;
	[ColumnIndex(1)]
	public uint? preStageAchievementId;
	public uint titleTextMapHash;
	public uint descTextMapHash;
	public ShowType isShow;
	public uint ps4GroupId;
	public uint ps5GroupId;
	public uint ps5TitleTextMapHash;
	public string ttype;
	public string psTrophyId;
	public string ps4TrophyId;
	public string ps5TrophyId;
	public string icon;
	[ColumnIndex(2)]
	public uint finishRewardId;
	[ColumnIndex(3)]
	public bool isDeleteWatcherAfterFinish;
	public ProgressShowType progressShowType;
}