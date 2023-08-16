namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class LanV2FireworksChallengeDataExcelConfig
{
	[ColumnIndex(0)]
	public uint challengeId;
	[ColumnIndex(1)]
	public string titleText;
	//public uint titleTextMapHash;
	[ColumnIndex(2)]
	public string icon;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] factorIdList;
	[ColumnIndex(4)]
	public uint initFireElementValue;
	[ColumnIndex(5)]
	public uint fullScore;
	[ColumnIndex(6)]
	public uint unlockAbilityScore;
	[ColumnIndex(7)]
	public uint unlockFireworksScore;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] watcherIdList;
	[ColumnIndex(9)]
	public uint addStaminaValue;
}