namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class LanV2OverAllDataExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	[ColumnIndex(2)]
	public uint bossDungeonId;
	[ColumnIndex(3)]
	public uint bossRewardId;
	[ColumnIndex(4)]
	public uint bossStartDay;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] bossWatcherIdList;
	[ColumnIndex(6)][TsvArray(',')]
    public uint[] bossExhibitionIdList;
	public uint bossPushTipsId;
	[ColumnIndex(7)][TsvArray(',')]
    public uint[] clothesWatcherIdList;
	[ColumnIndex(8)]
	public uint clothesRewardId;
	[ColumnIndex(9)][TsvArray(',')]
    public uint[] bossCardIdList;
	[ColumnIndex(10)]
	public uint activityPlayDuration;
	public bool hideExchangeEntry;
}