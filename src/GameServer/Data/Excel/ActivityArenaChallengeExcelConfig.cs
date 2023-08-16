namespace Weedwacker.GameServer.Data.Excel;

[Columns(8)]
public class ActivityArenaChallengeExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint scheduleId;
	[ColumnIndex(2)]
	public uint arenaChallengeId;
	[ColumnIndex(3)]
	public uint arenaChallengeLevel;
	[ColumnIndex(4)]
	public bool isExtraLevel;
	[ColumnIndex(5)]
	public uint watcherId;
	public uint challengeTargetTextMapHash;
	public uint challengeTarget1TextMapHash;
	public uint challengeTarget2TextMapHash;
	public string challengeDesc;
	public string challengeDesc1;
	public string challengeDesc2;

	//Not in client. Seems to be split to ActivityArenaChallengeLevelInfo
	[ColumnIndex(6)]
	public uint? monsterConfig;
	[ColumnIndex(7)]
	public uint GalleryID;
}