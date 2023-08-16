namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class EchoShellPreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint activityId;
	public uint specialVoiceID;
	public uint specialVoiceUnlockCondID;
	public uint specialVoiceLockTipTextMapHash;
	public bool isBetaBlocked;
}