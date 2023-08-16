namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class ActivityGachaBaseExcelConfig
{
	[ColumnIndex(0)]
	public uint activityId;
	[ColumnIndex(1)]
	public uint materialId;
	[ColumnIndex(2)]
	public uint elemTime;
	[ColumnIndex(3)]
	public uint taskContentId;
	[ColumnIndex(4)]
	public uint unlockStageId;
	[ColumnIndex(5)]
	public uint maxConvert;
	[ColumnIndex(6)]
	public uint robotLimit;
	[ColumnIndex(7)]
	public uint robotGuarNum;
	[ColumnIndex(8)]
	public uint robotHiddenFirstGuarNum;
	[ColumnIndex(9)]
	public uint robotHiddenGuarNum;
	[ColumnIndex(10)]
	public uint robotGuarRate;
	[ColumnIndex(11)]
	public uint robotHiddenGuarRate;
	[ColumnIndex(12)][TsvArray(',')]
	public uint[] watcherList;
	public uint[] questList;
	public uint reminderId;
	public uint exchangeTipsCond;
	public uint freeModeUnlockQuest;
}