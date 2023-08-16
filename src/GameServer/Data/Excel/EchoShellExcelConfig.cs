using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class EchoShellExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public EchoShellType? echoShellType;
	[ColumnIndex(2)]
	public uint storyId;
	public uint voiceTitleTextMapHash;
	public uint voiceDescTextMapHash;
	public uint[] voiceList;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[]? reminderList;
	public string imagePath;
	public uint markID;
	public float[] markPosition;
	public uint markTipTitleTextMapHash;
	public uint markTipContentTextMapHash;
	public uint[] exclusiveSceneTags;
	[ColumnIndex(4)]
	public uint? relatedDungeonID;
	public uint dungeonPassedLockTipTextMapHash;
	public uint relatedDungeonEntryID;
	public uint orderID;
	public uint[] relatedDungeonEntryIDList;
	public uint relatedSubQuestID;
}