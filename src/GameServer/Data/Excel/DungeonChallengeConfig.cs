using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(14)]
public class DungeonChallengeConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint targetTextTemplateTextMapHash;
	public uint subTargetTextTemplateTextMapHash;
	public uint progressTextTemplateTextMapHash;
	public uint subProgressTextTemplateTextMapHash;
	public string iconPath;
	[ColumnIndex(1)]
	public ChallengeType challenge_type;
	public bool isForwardTiming;
	[ColumnIndex(2)]
	public ChallengeRecordType? recordType;
	public bool noSuccessHint;
	public bool noFailHint;
	public bool noPauseHint;
	[ColumnIndex(3)]
	public InterruptButtonType? interruptButtonType;
	[ColumnIndex(4)]
	public bool isTransBackWhenInterrupt;
	public bool isSuccessWhenNotSettled;
	public bool isBlockTopTimer;
	[ColumnIndex(7)]
	public SubChallengeFadeOutType? subChallengeFadeOutRule;
	public float subChallengeFadeOutDelayTime;
	[ColumnIndex(8)]
	public SubChallengeBannerType? subChallengeBannerRule;
	[ColumnIndex(5)]
	public uint? activitySkillID;
	[ColumnIndex(9)][TsvArray(',')]
	public string[] teamAbilityGroupList;

	//not in client
	[ColumnIndex(6)]
	public string AbilityGroup;
	[ColumnIndex(10)]
	public bool? animationOnSubchallengeStart;
	[ColumnIndex(11)]
	public bool animationOnSubchallengeSuccess;
	[ColumnIndex(12)]
	public bool animationOnSubchallengeFail;
	[ColumnIndex(13)]
	public bool sortSubChallenge;
}