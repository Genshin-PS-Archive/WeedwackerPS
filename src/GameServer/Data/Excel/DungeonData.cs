using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(37)]
public class DungeonData
{
	[ColumnIndex(0)]
	public uint id;
	public ulong nameTextMapHash;
	public ulong displayNameTextMapHash;
	public ulong descTextMapHash;
	[ColumnIndex(1)]
	public DungeonType type;
	[ColumnIndex(2)]
	public DungeonSubType? subType;
	[ColumnIndex(3)]
	public bool isDynamicLevel;
	[ColumnIndex(4)]
	public uint? serialId;
	[ColumnIndex(5)]
	public DungeonPlayType? playType;
	[ColumnIndex(6)]
	public uint sceneId;
	[ColumnIndex(7)]
	public uint? eventInterval;
	[ColumnIndex(8)]
	public InvolveType involveType;
	[ColumnIndex(9)]
	public uint showLevel;
	[ColumnIndex(10)]
	public uint? avatarLimitType;
	[ColumnIndex(11)]
	public uint? limitLevel;
	[ColumnIndex(12)]
	public int levelRevise;
	[ColumnIndex(13)]
	public uint? prevDungeonId;
	[ColumnIndex(14)]
	public uint? requireAvatarId;
	[ColumnIndex(15)]
	public uint passCond;
	[ColumnIndex(16)]
	public uint? passJumpDungeon;
	[ColumnIndex(17)]
	public uint? reviveIntervalTime;
	[ColumnIndex(18)]
	public uint reviveMaxCount;
	[ColumnIndex(19)]
	public uint? dayEnterCount;
	[ColumnIndex(20)][TsvArray(1)]
	public IdCountConfig[] enterCostItems;
	[ColumnIndex(22)]
	public uint firstPassRewardPreviewID;
	public uint passRewardPreviewID;
	[ColumnIndex(23)]
	public uint settleCountdownTime;
	[ColumnIndex(24)]
	public uint failSettleCountdownTime;
	[ColumnIndex(25)]
	public uint quitSettleCountdownTime;
	[ColumnIndex(26)][TsvArray(4)]
	public SettleShowType?[] settleShows;
	[ColumnIndex(30)]
	public bool forbiddenRestart;
	[ColumnIndex(31)]
	public SettleUIType settleUIType;
	public uint statueCostID;
	[ColumnIndex(32)]
	public uint statueCostCount;
	[ColumnIndex(33)]
	public uint statueDrop;
	public ElementType[] recommendElementTypes;
	public string[] recommendTips;
	[ColumnIndex(34)][TsvDictionary(':', ',')]
	public Dictionary<uint, uint> levelConfigMap;
	public uint[] previewMonsterList;
	public uint gearDescTextMapHash;
	[ColumnIndex(35)]
	public uint cityID;
	public bool dontShowPushTips;
	public string entryPicPath;
	[ColumnIndex(36)]
	public DungeonStateType stateType;
	public string factorPic;
	public string factorIcon;
	public bool enableQuestGuide;
}
