namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class RogueDiaryPreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint activityId;
	public bool blockSwitch;
	public uint dungeonEntryId;
	public uint dungeonMarkId;
	public uint roomMarkId;
	public uint[] preWQIdList;
	public uint[] hiddenWQIdList;
	public uint[] preQuestIdList;
	public uint changeEntryCondId;
	[ColumnIndex(1)]
	public uint roomSceneId;
	[ColumnIndex(2)]
	public uint worldSceneId;
}