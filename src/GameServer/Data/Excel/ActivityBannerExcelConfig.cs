using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class ActivityBannerExcelConfig
{
	[ColumnIndex(0)]
	public uint activityId;
	[ColumnIndex(1)]
	public ActivityBannerType? bannerType;
	[ColumnIndex(2)]
	public uint? rewardPreviewId;
	[ColumnIndex(3)]
	public string jsonConfigName;
	[ColumnIndex(4)]
	public string prefabPath;
	public bool showDesc;
	public uint descTextMapHash;
}