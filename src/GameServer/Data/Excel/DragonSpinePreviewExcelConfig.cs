namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class DragonSpinePreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	public uint descTextMapHash;
	public uint preQuestId;
	[ColumnIndex(2)]
	public uint unlockLevel;
	[ColumnIndex(3)]
	public uint rewardPreviewId;
	[ColumnIndex(4)]
	public uint contentDuration;
	[ColumnIndex(5)]
	public uint questId;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] questIdList;
}