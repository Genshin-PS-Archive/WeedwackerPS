namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class BuoyantCombatLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint galleryId;
	public uint groupId;
	[ColumnIndex(2)]
	public uint dayIndex;
	public uint rewardPreview;
	public uint levelRuleTextMapHash;
	public uint levelRuleBriefTextMapHash;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] watcherID;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] watcherScore;
	public uint[] recommendLevel;
	public float[] iconPosition;
}