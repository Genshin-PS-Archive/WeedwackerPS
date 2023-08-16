using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class DialogExcelConfig
{
	public uint nextDialog;
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] nextDialogs;
	[ColumnIndex(2)]
	public TalkShowType? talkShowType;
	[ColumnIndex(3)]
	public TalkRole talkRole;
	public uint talkContentTextMapHash;
	public uint talkTitleTextMapHash;
	public uint talkRoleNameTextMapHash;
	public string talkAssetPath;
	public string talkAssetPath_Alter;
	public string talkAudioName;
	public string actionBefore;
	public string actionWhile;
	public string actionAfter;
	public float showDuration;
	public uint groupId;
	public string optionIcon;

	[Columns(2)]
	public class TalkRole
	{
		[ColumnIndex(0)]
		public TalkRoleType? type;
		[ColumnIndex(1)]
		public string? id;
	}

	//not in client
	[ColumnIndex(5)]
	public uint? emotionModuleID;
	[ColumnIndex(6)]
	public uint? actionID;
	[ColumnIndex(7)]
	public string eyeExpressionPath;
	[ColumnIndex(8)]
	public bool doBlink;
}