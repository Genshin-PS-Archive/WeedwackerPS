namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class ActivityHachiStageExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public uint stageId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] questId;
	public uint questTitleTextMapHash;
	public uint questDescTextMapHash;
	public uint stealthTitleTextMapHash;
	public uint battleDescTextMapHash;
	[ColumnIndex(3)]
	public uint stealthWatcher;
	[ColumnIndex(4)]
	public uint battleWatcher;
	[ColumnIndex(5)]
	public string stealthGroup;
	[ColumnIndex(6)]
	public string battleGroup;
	public uint stealthPushTips;
	public uint battlePushTips;
	public uint pushTip;
	[ColumnIndex(7)]
	public uint openDay;
	public float[] stealthTriggerPointMarkPos;
	public float[] stealthMarkPos;
	public float[] battleMarkPos;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] finalQuestId;
	[ColumnIndex(9)][TsvArray(',')]
	public uint[] mapMarkLoadQuest;
	public uint stealthChallengeIndex;
	public uint battleChallengeIndex;
	public uint stealthRadius;
	public uint battleRadius;
}