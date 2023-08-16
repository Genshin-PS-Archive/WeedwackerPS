using System.Collections.Concurrent;
using System.Text.RegularExpressions;
using NLua;
using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Data.BinOut.AbilityGroup;
using Weedwacker.GameServer.Data.BinOut.Avatar;
using Weedwacker.GameServer.Data.BinOut.Gadget;
using Weedwacker.GameServer.Data.BinOut.GadgetPath;
using Weedwacker.GameServer.Data.BinOut.Quest;
using Weedwacker.GameServer.Data.BinOut.Scene.Point;
using Weedwacker.GameServer.Data.BinOut.Scene.SceneNpcBorn;
using Weedwacker.GameServer.Data.BinOut.Talent;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Systems.Script.Scene;
using Weedwacker.Shared.Utils;
using Newtonsoft.Json;
using System.Diagnostics;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data;

internal static class GameData
{
    #region Avatar
    public readonly static ConcurrentDictionary<uint, AvatarCodexData> AvatarCodexDataMap = new(); // sortId
    public readonly static ConcurrentDictionary<uint, AvatarCostumeData> AvatarCostumeDataMap = new(); // costumeId
    public readonly static ConcurrentDictionary<uint, AvatarCurveData> AvatarCurveDataMap = new(); // level
    public readonly static ConcurrentDictionary<uint, AvatarData> AvatarDataMap = new(); // AvatarId
    public readonly static ConcurrentDictionary<uint, AvatarExtraPropertyExcelConfig> AvatarExtraPropertyDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, AvatarFetterLevelData> AvatarFetterLevelDataMap = new(); // level
    public readonly static ConcurrentDictionary<uint, AvatarFlycloakData> AvatarFlycloakDataMap = new(); // flycloakId
    public readonly static ConcurrentDictionary<uint, AvatarLevelData> AvatarLevelDataMap = new(); // Level
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, AvatarPointExcelConfig> AvatarPointDataMap = new(); // Level
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, AvatarPromoteData> AvatarPromoteDataMap = new(); // <<PromoteId, promoteLevel> ,Data>
    public readonly static ConcurrentDictionary<uint, AvatarReplaceCostumeExcelConfig> AvatarReplaceCostumeDataMap = new(); // avatarId
    public readonly static ConcurrentDictionary<uint, AvatarSkillData> AvatarSkillDataMap = new(); // SkillId
    public readonly static ConcurrentDictionary<uint, AvatarSkillDepotData> AvatarSkillDepotDataMap = new(); // SkillDepotId
    public readonly static ConcurrentDictionary<string, Dictionary<string, ConfigTalentMixin[]>> AvatarTalentConfigDataMap = new(); // file name
    public readonly static ConcurrentDictionary<uint, AvatarTalentData> AvatarTalentDataMap = new(); // talentId
    public readonly static ConcurrentDictionary<uint, FetterCharacterCardData> FetterCharacterCardDataMap = new(); // avatarId
    public readonly static ConcurrentDictionary<uint, FetterInfoData> FetterInfoDataMap = new(); // fetterId
    public readonly static ConcurrentDictionary<uint, FettersData> FettersDataMap = new(); // fetterId
    public readonly static ConcurrentDictionary<uint, FetterStoryData> FetterStoryDataMap = new(); // fetterId
    public readonly static ConcurrentDictionary<uint, PhotographExpressionData> PhotographExpressionDataMap = new(); // fetterId
    public readonly static ConcurrentDictionary<uint, PhotographPosenameData> PhotographPosenameDataMap = new(); // fetterId
    public readonly static ConcurrentDictionary<uint, ProudSkillData> ProudSkillDataMap = new(); // proudSkillId
    #endregion

    #region A
    public readonly static ConcurrentDictionary<string, ConfigAbilityGroup> AbilityGroupDataMap = new(); // skillDepotAbilityGroup name
    public readonly static ConcurrentDictionary<uint, AbilityOverrideExcelConfig> AbilityOverrideDataMap = new(); // id
    public readonly static ConcurrentDictionary<string, AbilityPropExcelConfig> AbilityPropDataMap = new(); // propName
    public readonly static ConcurrentDictionary<uint, AbilityStateResistanceByIDExcelConfig> AbilityStateResistanceByIDMap = new(); // id
    public readonly static ConcurrentDictionary<uint, AchievementExcelConfig> AchievementDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, AchievementGoalExcelConfig> AchievementGoalDataMap = new(); // orderId
    public readonly static ConcurrentDictionary<uint, ActivityAbilityGroupExcelConfig> ActivityAbilityGroupMap = new(); // index
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, ActivityArenaChallengeExcelConfig> ActivityArenaChallengeDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, ActivityBannerExcelConfig> ActivityBannerDataMap = new(); // activityId
    public readonly static ConcurrentDictionary<uint, ActivityChessAffixExcelConfig> ActivityChessAffixDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, ActivityChessCardExcelConfig> ActivityChessCardDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, ActivityChessGearExcelConfig> ActivityChessGearDataMap = new(); // gearID
    public readonly static ConcurrentDictionary<uint, ActivityChessLevelExcelConfig> ActivityChessLevelDataMap = new(); // level
    public readonly static ConcurrentDictionary<uint, ActivityChessMapExcelConfig> ActivityChessMapDataMap = new(); // chessMapID
    public readonly static ConcurrentDictionary<uint, ActivityChessMapPointExcelConfig> ActivityChessMapPointDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, ActivityChessPreviewExcelConfig> ActivityChessPreviewDataMap = new(); // activityID
    public readonly static ConcurrentDictionary<uint, ActivityChessScheduleExcelConfig> ActivityChessScheduleDataMap = new(); //day
    public readonly static ConcurrentDictionary<uint, ActivityCrystalLinkCondBuffExcelConfig> ActivityCrystalLinkCondBuffMap = new(); //buffId
    public readonly static ConcurrentDictionary<uint, ActivityCrystalLinkDifficultyExcelConfig> ActivityCrystalLinkDifficultyDataMap = new(); //difficultyId
    public readonly static ConcurrentDictionary<uint, ActivityCrystalLinkEffectBuffExcelConfig> ActivityCrystalLinkEffectBuffMap = new(); //buffId
    public readonly static ConcurrentDictionary<uint, ActivityCrystalLinkLevelExcelConfig> ActivityCrystalLinkLevelDataMap = new(); //levelId
    public readonly static ConcurrentDictionary<uint, ActivityDeliveryDailyExcelConfig> ActivityDeliveryDailyDataMap = new(); // dailyConfigId
    public readonly static ConcurrentDictionary<uint, ActivityDeliveryExcelConfig> ActivityDeliveryDataMap = new(); // scheduleId
    public readonly static ConcurrentDictionary<uint, ActivityDeliveryWatcherDataConfig> ActivityDeliveryWatcherDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityGachaBaseExcelConfig> ActivityGachaBaseDataMap = new(); // activityId
    public readonly static ConcurrentDictionary<uint, ActivityGachaRobotExcelConfig> ActivityGachaRobotDataMap = new(); // robotId
    public readonly static ConcurrentDictionary<uint, ActivityGachaStageExcelConfig> ActivityGachaStageDataMap = new(); // stageId
    public readonly static ConcurrentDictionary<uint, ActivityGachaTarget> ActivityGachaTargetDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, ActivityGearExcelConfig> ActivityGearDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityGearGadgetGearExcelConfig> ActivityGearGadgetGearDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityGearGadgetShaftExcelConfig> ActivityGearGadgetShaftDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityGearJigsawExcelConfig> ActivityGearJigsawDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityGearLevelExcelConfig> ActivityGearLevelDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityGroupLinksExcelConfig> ActivityGroupLinksDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityHachiFinalStageExcelConfig> ActivityHachiFinalStageDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, ActivityHachiStageExcelConfig> ActivityHachiStageDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, ActivityHideAndSeekBasicConfig> ActivityHideAndSeekDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityJigsawDetailExcelConfig> ActivityJigsawDetailDataMap = new(); // PuzzleID
    public readonly static ConcurrentDictionary<uint, ActivityPhotographExcelConfig> ActivityPhotographDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityPhotographPosExcelConfig> ActivityPhotographPosDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivityPlantFlowerDailyExcelConfig> ActivityPlantFlowerDailyMap = new(); // dailyConfigId
    public readonly static ConcurrentDictionary<uint, ActivityPlantFlowerMainExcelConfig> ActivityPlantFlowerMainMap = new(); // scheduleId
    public readonly static ConcurrentDictionary<uint, ActivityShopOverallExcelConfig> ActivityShopOverallDataMap = new(); // scheduleId
    public readonly static ConcurrentDictionary<uint, ActivityShopSheetExcelConfig> ActivityShopSheetDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ActivitySkillExcelConfig> ActivitySkillDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, AnimalCodexExcelConfig> AnimalCodexDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, AnimalDescribeExcelConfig> AnimalDescribeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, AranaraCollectionExcelConfig> AranaraCollectionDataMap = new(); // CollectionID
    public readonly static ConcurrentDictionary<uint, AsterActivityPreviewExcelConfig> AsterActivityPreviewDataMap = new(); // activityId
    public readonly static ConcurrentDictionary<uint, AsterLittleExcelConfig> AsterLittleDataMap = new(); // stage_id
    public readonly static ConcurrentDictionary<uint, AsterMidDifficultyExcelConfig> AsterMidDifficultyDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, AsterMidExcelConfig> AsterMidDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, AsterMidGroupsExcelConfig> AsterMidGroupsDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, AsterMissionExcelConfig> AsterMissionDataMap = new(); // missionId
    public readonly static ConcurrentDictionary<uint, AsterStageExcelConfig> AsterStageDataMap = new(); // id
    public readonly static ConcurrentDictionary<string, AttackAttenuationExcelConfig> AttackAttenuationDataMap = new(); // group
    public readonly static ConcurrentDictionary<uint, AvatarCardChangeExcelConfig> AvatarCardChangeDataMap = new(); //AvatarId
    public readonly static ConcurrentDictionary<uint, AvatarHeroEntityData> AvatarHeroEntityDataMap = new(); //avatarId
    #endregion
    #region B
    public readonly static ConcurrentDictionary<uint, BargainExcelConfig> BargainDataMap = new(); //id
    public readonly static ConcurrentDictionary<uint, BartenderAffixExcelConfig> BartenderAffixDataMap = new(); //id
    public readonly static ConcurrentDictionary<uint, BartenderBasicExcelConfig> BartenderBasisDataMap = new(); //activityId
    public readonly static ConcurrentDictionary<Tuple<BartenderEffectType, uint?, string>, BartenderEventExcelConfig> BartenderEventDataMap = new(); //<miscId, effectName>
    public readonly static ConcurrentDictionary<uint, BartenderFormulaExcelConfig> BartenderFormulaDataMap = new(); //id
    public readonly static ConcurrentDictionary<uint, BartenderOrderExcelConfig> BartenderOrderDataMap = new(); //id
    public readonly static ConcurrentDictionary<uint, BartenderTaskExcelConfig> BartenderTaskDataMap = new(); //id
    public readonly static ConcurrentDictionary<uint, BartenderTaskOrderExcelConfig> BartenderTaskOrderDataMap = new(); //questId
    public readonly static ConcurrentDictionary<uint, BattlePassLevelExcelConfig> BattlePassLevelMap = new(); //id
    public readonly static ConcurrentDictionary<uint, BattlePassMissionExcelConfig> BattlePassMissionMap = new(); //id
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, BattlePassRewardExcelConfig> BattlePassRewardMap = new(); // <indexId, level>
    public readonly static ConcurrentDictionary<uint, BattlePassScheduleExcelConfig> BattlePassScheduleMap = new(); //id
    public readonly static ConcurrentDictionary<uint, BirthdayMailExcelConfig> BirthdayMailDataMap = new(); //id
    public readonly static ConcurrentDictionary<uint, BlessingScanExcelConfig> BlessingScanDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlessingScanDropExcelConfig> BlessingScanDropDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlessingScanPicExcelConfig> BlessingScanPicDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlessingScanTypeExcelConfig> BlessingScanTypeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlitzRushExcelConfig> BlitzRushOverAllMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlitzRushParkourExcelConfig> BlitzRushParkourMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlitzRushStageExcelConfig> BlitzRushStageMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlossomChestData> BlossomChestDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlossomGroupsExcelConfig> BlossomGroupsDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlossomOpenExcelConfig> BlossomOpenDataMap = new(); // cityId
    public readonly static ConcurrentDictionary<uint, BlossomRefreshExcelConfig> BlossomRefreshDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlossomReviseExcelConfig> BlossomReviseDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlossomSectionOrderExcelConfig> BlossomSectionOrderDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BlossomTalkExcelConfig> BlossomTalkDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BonusActivityExcelConfig> BonusActivityDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BonusTreasureSolutionExcelConfig> BonusTreasureSolutionDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BooksCodexExcelConfig> BooksCodexDataMap = new(); // Id
    public readonly static ConcurrentDictionary<BoredActionType, BoredActionPriorityExcelConfig> BoredActionPriorityMap = new(); // action_type
    public readonly static ConcurrentDictionary<uint, BoredCreateMonsterExcelConfig> BoredCreateMonsterMap = new(); // player_level
    public readonly static ConcurrentDictionary<BoardEventType, BoredEventExcelConfig> BoredEventMap = new(); // event_type
    public readonly static ConcurrentDictionary<uint, BoredMonsterPoolConfig> BoredMonsterPoolMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BounceConjuringChapterExcelConfig> BounceConjuringChapterDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BounceConjuringPreviewExcelConfig> BounceConjuringPreviewDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BuffExcelConfig> BuffDataMap = new(); // serverBuffId
    public readonly static ConcurrentDictionary<uint, BuoyantCombatExcelConfig> BuoyantCombatDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BuoyantCombatLevelExcelConfig> BuoyantCombatLevelDataMap = new(); // id
    #endregion
    #region C
    public readonly static ConcurrentDictionary<uint, CaptureExcelConfig> CaptureDataMap = new(); // monsterID
    public readonly static ConcurrentDictionary<uint, CaptureTagsExcelConfig> CaptureTagsDataMap = new(); // captureTagID
    public readonly static ConcurrentDictionary<uint, CatalogExcelConfig> CatalogDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, CharAmusementLevelExcelConfig> CharAmusementLevelDataMap = new(); // stage_id
    public readonly static ConcurrentDictionary<uint, CharAmusementOverallExcelConfig> CharAmusementOverallDataMap = new(); // scheduleID
    public readonly static ConcurrentDictionary<uint, CharAmusementStageExcelConfig> CharAmusementStageDataMap = new(); // activity_stage
    public readonly static ConcurrentDictionary<uint, ChapterExcelConfig> ChapterDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ChargeBarStyleExcelConfig> ChargeBarStyleDataMap = new(); // id
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, ChestDropExcelConfig> ChestDropDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, CityConfig> CityDataMap = new(); // cityId
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, CityLevelupConfig> CityLevelupDataMap = new(); // city_id
    public readonly static ConcurrentDictionary<uint, CityTaskOpenExcelConfig> CityTaskOpenDataMap = new(); // cityId
    public readonly static ConcurrentDictionary<uint, CombineExcelConfig> CombineDataMap = new(); // combineId
    public readonly static ConcurrentDictionary<uint, ComponentExcelConfig> ComponentDataMap = new(); // componentID
    public readonly static ConcurrentDictionary<uint, CompoundBoostExcelConfig> CompoundBoostDataMap = new(); // materialID
    public readonly static ConcurrentDictionary<uint, CompoundExcelConfig> CompoundDataMap = new(); // id
    public readonly static ConcurrentDictionary<string, ConfigAbility> ConfigAbilityMap = new(); // abilityName
    public readonly static ConcurrentDictionary<string, ConfigAvatar> ConfigAvatarMap = new(); //filename
    public readonly static ConcurrentDictionary<string, ConfigGadget> ConfigGadgetMap = new(); //filename
    public readonly static ConcurrentDictionary<ConstValueType, ConstValueExcelConfig> ConstValueDataMap = new(); // name
    public readonly static ConcurrentDictionary<uint, ConvertExcelConfig> ConvertDataMap = new(); // ConvertID
    public readonly static ConcurrentDictionary<uint, CookBonusExcelConfig> CookBonusDataMap = new(); // <avatarId, recipeId>
    public readonly static ConcurrentDictionary<uint, CookRecipeExcelConfig> CookRecipeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, CoopCGExcelConfig> CoopCGDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, CoopChapterExcelConfig> CoopChapterDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, CoopInteractionExcelConfig> CoopInteractionDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, CoopPointExcelConfig> CoopPointDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, CoopRewardExcelConfig> CoopRewardDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, CustomLevelDungeonConfig> CustomLevelDungeonDataMap = new(); // dungeonID
    public readonly static ConcurrentDictionary<uint, CustomLevelGroupConfig> CustomLevelGroupDataMap = new(); // groupId
    public readonly static ConcurrentDictionary<uint, CustomLevelTagConfig> CustomLevelTagDataMap = new(); // configId
    public readonly static ConcurrentDictionary<uint, CusmtomGadgetConfigIdExcelConfig> CustomGadgetConfigIdDataMap = new(); // configId
    public readonly static ConcurrentDictionary<uint, CustomGadgetSlotLevelTagConfig> CustomGadgetSlotLevelTagDataMap = new(); //ID
    public readonly static ConcurrentDictionary<uint, CustomGadgetRootExcelConfig> CustomGadgetRootDataMap = new(); //rootGadgetID
    public readonly static ConcurrentDictionary<uint, CusmtomGadgetSlotExcelConfig> CustomGadgetSlotDataMap = new(); //slotID
    public readonly static ConcurrentDictionary<uint, CutsceneExcelConfig> CutsceneDataMap = new(); //id
    #endregion
    #region D
    public readonly static ConcurrentDictionary<uint, DailyDungeonConfig> DailyDungeonDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DailyTaskExcelConfig> DailyTaskDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, DailyTaskLevelExcelConfig> DailyTaskLevelDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, DailyTaskRewardExcelConfig> DailyTaskRewardDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, DeathRegionLevelExcelConfig> DeathRegionLevelDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, DeshretObeliskArgumentExcelConfig> DeshretObeliskArgumentDataMap = new(); // addedValueID
    public readonly static ConcurrentDictionary<uint, DialogExcelConfig> DialogDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DigGroupLinkExcelConfig> DigGroupLinkDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DocumentExcelConfig> DocumentDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DraftExcelConfig> DraftDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DragonSpineEnhanceExcelConfig> DragonSpineEnhanceDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DragonSpineMissionExcelConfig> DragonSpineMissionDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DragonSpinePreviewExcelConfig> DragonSpinePreviewDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DragonSpineStageExcelConfig> DragonSpineStageDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, BaseDropIndexConfig> DropIndexDataMap = new(); // dropId
    public readonly static ConcurrentDictionary<uint, DropSubfieldExcelConfig> DropSubfieldDataMap = new(); // branchDropPoolId
    public readonly static ConcurrentDictionary<uint, DungeonChallengeConfig> DungeonChallengeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DungeonData> DungeonDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DungeonElementChallengeExcelConfig> DungeonElementChallengeDataMap = new(); // dungeonId
    public readonly static ConcurrentDictionary<uint, DungeonEntryExcelConfig> DungeonEntryDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, IList<string>> DungeonLevelEntityDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DungeonMapAreaExcelConfig> DungeonMapAreaMap = new(); // dungeonID
    public readonly static ConcurrentDictionary<uint, DungeonPassExcelConfig> DungeonPassDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DungeonRosterConfig> DungeonRosterDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, DungeonSerialConfig> DungeonSerialDataMap = new(); // id
    #endregion
    #region E
    public readonly static ConcurrentDictionary<uint, EchoShellFloatSignalExcelConfig> EchoShellFloatSignalMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EchoShellPreviewExcelConfig> EchoShellPreviewMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EchoShellSummerTimeDungeonExcelConfig> EchoShellSummerTimeDungeonMap = new(); // dungeonId
    public readonly static ConcurrentDictionary<uint, EchoShellRewardExcelConfig> EchoShellRewardDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EchoShellExcelConfig> EchoShellVoiceDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EffigyChallengeExcelConfig> EffigyChallengeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EffigyChallengeV2ExcelConfig> EffigyChallengeV2DataMap = new(); // ConfigID
    public readonly static ConcurrentDictionary<uint, EffigyChallengeV2SkillExcelConfig> EffigyChallengeV2SkillDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EffigyChallengeV2OverallExcelConfig> EffigyChallengeV2OverallDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EffigyDifficultyExcelConfig> EffigyDifficultyDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EffigyDifficultyV2ExcelConfig> EffigyDifficultyV2DataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EffigyLimitingConditionExcelConfig> EffigyLimitingConditionDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EffigyRewardExcelConfig> EffigyRewardDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ElementCoeffExcelConfig> ElementDataMap = new(); // level
    public readonly static ConcurrentDictionary<ElementType, ElementStateExcelConfig> ElementStateDataMap = new(); // elementType
    public readonly static ConcurrentDictionary<EndureType, EndureTemplateExcelConfig> EndureTemplateDataMap = new(); // type
    public readonly static ConcurrentDictionary<uint, EntityDropSubfieldExcelConfig> EntityDropSubfieldDataMap = new(); // EntityId
    public readonly static ConcurrentDictionary<uint, EntityMultiPlayerExcelConfig> EntityMultiPlayerFixDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, EnvAnimalGatherData> EnvAnimalGatherDataMap = new(); // animalId
    public readonly static ConcurrentDictionary<EnvironmentType, EnvAnimalWeightExcelConfig> EnvAnimalWeightDataMap = new(); // envType
    public readonly static ConcurrentDictionary<uint, EquipAffixData> EquipAffixDataMap = new(); // affixId
    public readonly static ConcurrentDictionary<uint, ExclusiveRuleExcelConfig> ExclusiveRuleDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExhibitionListExcelConfig> ExhibitionListDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExhibitionDisplayExcelConfig> ExhibitionDisplayDataMap = new(); // DisplayID
    public readonly static ConcurrentDictionary<uint, ExhibitionLuaStorageExcelConfig> ExhibitionLuaStorageDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, ExhibitionScoreExcelConfig> ExhibitionScoreDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExhibitionSeriesExcelConfig> ExhibitionSeriesDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExpeditionBonusExcelConfig> ExpeditionBonusDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExpeditionChallengeExcelConfig> ExpeditionChallengeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExpeditionDataExcelConfig> ExpeditionDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, ExpeditionDifficultyExcelConfig> ExpeditionDifficultyDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, ExpeditionActivityMarkerExcelConfig> ExpeditionMarkDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExpeditionPathExcelConfig> ExpeditionPathDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExpeditionActivityPreviewExcelConfig> ExpeditionPreviewDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ExploreAreaTotalExpExcelConfig> ExploreAreaTotalExpDataMap = new(); // areaID
    public readonly static ConcurrentDictionary<uint, ExploreExcelConfig> ExploreMaterialDataMap = new(); // materialID
    #endregion
    #region F
    public readonly static ConcurrentDictionary<uint, FeatureTagExcelConfig> FeatureTagDataMap = new(); // tagId
    public readonly static ConcurrentDictionary<uint, FeatureTagGroupExcelConfig> FeatureTagGroupDataMap = new(); // groupID
    public readonly static ConcurrentDictionary<uint, FireworksExcelConfig> FireworksDataMap = new(); // materialID
    public readonly static ConcurrentDictionary<FireworksLaunchParamType, FireworksLaunchExcelConfig> FireworksLaunchDataMap = new(); // launchParamType
    public readonly static ConcurrentDictionary<uint, FishBaitExcelConfig> FishBaitDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FishExcelConfig> FishDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FishPoolExcelConfig> FishPoolDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FishRodExcelConfig> FishRodDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FishSkillExcelConfig> FishSkillDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FishStockExcelConfig> FishStockDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FleurFairBuffEnergyStatExcelConfig> FleurFairBuffEnergyStatDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FleurFairChapterExcelConfig> FleurFairChapterDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FleurFairDungeonExcelConfig> FleurFairDungeonDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FleurFairDungeonStatExcelConfig> FleurFairDungeonStatDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FleurFairMiniGameExcelConfig> FleurFairMiniGameDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FleurFairPreviewExcelConfig> FleurFairPreviewDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FlightActivityExcelConfig> FlightActivityDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FlightActivityDayExcelConfig> FlightActivityDayDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FlightActivityMedalExcelConfig> FlightActivityMedalDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ForgeExcelConfig> ForgeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ForgeUpdateExcelConfig> ForgeUpdateDataMap = new(); // playerLevel
    public readonly static ConcurrentDictionary<uint, FungusBaseExcelConfig> FungusFighterBaseMap = new(); // activityId
    public readonly static ConcurrentDictionary<uint, FungusCampExcelConfig> FungusFighterCatchLevelMap = new(); // campId
    public readonly static ConcurrentDictionary<uint, FungusCultivateExcelConfig> FungusFighterCultivateMap = new(); // levelId
    public readonly static ConcurrentDictionary<uint, FungusFighterDungeonExcelConfig> FungusFighterDungeonMap = new(); // DungeonID
    public readonly static ConcurrentDictionary<uint, FungusFighterDungeonQSTExcelConfig> FungusFighterDungeonQSTMap = new(); // plotID
    public readonly static ConcurrentDictionary<uint, FungusFighterMonsterExcelConfig> FungusFighterMonsterMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FungusNameExcelConfig> FungusFighterNameMap = new(); // id
    public readonly static ConcurrentDictionary<uint, FurnitureMakeExcelConfig> FurnitureMakeDataMap = new(); // configID
    public readonly static ConcurrentDictionary<uint, FurnitureSuiteExcelConfig> FurnitureSuiteDataMap = new(); // suiteID
    public readonly static ConcurrentDictionary<uint, HomeWorldFurnitureTypeExcelConfig> FurnitureTypeDataMap = new(); // typeID
    #endregion
    #region G
    public readonly static ConcurrentDictionary<uint, GachaNewbieExcelConfig> GachaNewbieDataMap = new(); // SinglePullItemID
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, GachaPoolExcelConfig> GachaPoolDataMap = new(); // <PoolID, ItemID>
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, GachaProbExcelConfig> GachaProbDataMap = new(); // <ProbabilityRuleID, PropParentType>
    public readonly static ConcurrentDictionary<uint, GachaRestrictExcelConfig> GachaRestrictDataMap = new(); // GachaPoolID
    public readonly static ConcurrentDictionary<uint, GachaRuleExcelConfig> GachaRuleDataMap = new(); // RuleID
    public readonly static ConcurrentDictionary<uint, GachaTokenDropExcelConfig> GachaTokenDropDataMap = new(); // ItemSubType
    public readonly static ConcurrentDictionary<uint, GachaWishExcelConfig> GachaWishMap = new(); // GachaPoolID
    public readonly static ConcurrentDictionary<uint, GadgetChainExcelConfig> GadgetChainDataMap = new(); // chainId
    public readonly static ConcurrentDictionary<uint, GadgetCurveExcelConfig> GadgetCurveDataMap = new(); // level
    public readonly static ConcurrentDictionary<uint, GadgetData> GadgetDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, GadgetInteractExcelConfig> GadgetInteractDataMap = new(); // interactId
    public readonly static ConcurrentDictionary<uint, LandSoundExcelConfig> GadgetLandSoundMap = new(); // id
    public readonly static ConcurrentDictionary<uint, GadgetPropExcelConfig> GadgetPropDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, GalleryExcelConfig> GalleryDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, GatherBundleExcelConfig> GatherBundleDataMap = new(); // id
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, GatherData> GatherDataMap = new(); // <id, gadgetId>
    public readonly static ConcurrentDictionary<uint, GivingExcelConfig> GivingDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, GivingGroupExcelConfig> GivingItemListMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, GlobalWatcherConfig> GlobalWatcherDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, GravenCampExcelConfig> GravenCampDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, GravenCampPhotoExcelConfig> GravenCampPhotoDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, GravenCampRaceExcelConfig> GravenCampRaceDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, GravenCampStageExcelConfig> GravenCampStageDataMap = new(); // StageID
    public readonly static ConcurrentDictionary<uint, GravenCarveOverallExcelConfig> GravenCarveOverallDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, GravenCarveStageExcelConfig> GravenCarveStageDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, GravenInnocenceExcelConfig> GravenInnocenceDataMap = new(); // activityID
    public readonly static ConcurrentDictionary<uint, GravenPhotoObjectExcelConfig> GravenPhotoObjectDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, GroupLinksBundleExcelConfig> GroupLinksBundleDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, GroupLinksBundleRewardExcelConfig> GroupLinksBundleRewardDataMap = new(); // rewardId
    public readonly static ConcurrentDictionary<uint, GroupTagExcelConfig> GroupTagDataMap = new(); // id
    public static ConfigGadgetPath GadgetPathMap { get; private set; }
    public static ConfigGlobalCombat GlobalCombatData { get; private set; }
    #endregion
    #region H
    public readonly static ConcurrentDictionary<uint, H5ActivityExcelConfig> H5ActivityDataMap = new(); // h5ActivityId
    public readonly static ConcurrentDictionary<uint, H5ActivityWatcherExcelConfig> H5ActivityWatcherDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, HideAndSeekSkillExcelConfig> HideAndSeekSkillDataMap = new(); // id
    public readonly static ConcurrentDictionary<string, HitLevelTemplateExcelConfig> HitLevelTemplateDataMap = new(); // type
    public readonly static ConcurrentDictionary<uint, HomeworldAnimalExcelConfig> HomeworldAnimalDataMap = new(); // furnitureID
    public readonly static ConcurrentDictionary<uint, HomeWorldAreaComfortExcelConfig> HomeWorldAreaComfortDataMap = new(); // configID
    public readonly static ConcurrentDictionary<uint, HomeWorldBgmExcelConfig> HomeWorldBgmDataMap = new(); // bgmID
    public readonly static ConcurrentDictionary<uint, HomeworldBlueprintSlotExcelConfig> HomeworldBlueprintSlotDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, HomeWorldComfortLevelExcelConfig> HomeWorldComfortLevelDataMap = new(); // levelID
    public readonly static ConcurrentDictionary<uint, HomeWorldEventExcelConfig> HomeWorldEventDataMap = new(); // eventID
    public readonly static ConcurrentDictionary<uint, HomeWorldExtraFurnitureExcelConfig> HomeWorldExtraFurnitureDataMap = new(); // furnitureID
    public readonly static ConcurrentDictionary<uint, HomeWorldFarmFieldExcelConfig> HomeWorldFieldDataMap = new(); // fieldGadgetID
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, HomeWorldLeastShopExcelConfig> HomeWorldLeastShopDataMap = new(); // level
    public readonly static ConcurrentDictionary<uint, HomeworldLevelExcelConfig> HomeworldLevelDataMap = new(); // level
    public readonly static ConcurrentDictionary<uint, HomeWorldLimitShopExcelConfig> HomeWorldLimitShopDataMap = new(); // goodsId
    public readonly static ConcurrentDictionary<uint, HomeworldModuleExcelConfig> HomeworldModuleDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, HomeWorldNPCExcelConfig> HomeWorldNPCDataMap = new(); // furnitureID
    public readonly static ConcurrentDictionary<uint, HomeWorldPlantExcelConfig> HomeWorldPlantDataMap = new(); // seedID
    public readonly static ConcurrentDictionary<uint, HomeWorldServerGadgetExcelConfig> HomeWorldServerGadgetDataMap = new(); // furnitureId
    public readonly static ConcurrentDictionary<uint, HomeWorldShopSubTagExcelConfig> HomeWorldShopSubTagDataMap = new(); // subID
    public readonly static ConcurrentDictionary<uint, HomeWorldWoodExcelConfig> HomeWorldWoodDataMap = new(); // WoodID
    public readonly static ConcurrentDictionary<uint, HuntingClueGatherExcelConfig> HuntingClueGatherDataMap = new(); //configId
    public readonly static ConcurrentDictionary<uint, HuntingClueMonsterExcelConfig> HuntingClueMonsterDataMap = new(); //configId
    public readonly static ConcurrentDictionary<uint, HuntingGroupInfoExcelConfig> HuntingGroupInfoDataMap = new(); //groupId
    public readonly static ConcurrentDictionary<uint, HuntingMonsterExcelConfig> HuntingMonsterDataMap = new(); //groupId
    public readonly static ConcurrentDictionary<uint, HuntingRefreshExcelConfig> HuntingRefreshDataMap = new(); //id
    public readonly static ConcurrentDictionary<uint, HuntingRegionExcelConfig> HuntingRegionDataMap = new(); //id
    #endregion
    #region I
    public readonly static ConcurrentDictionary<uint, InferenceConclusionExcelConfig> InferenceConclusionDataMap = new(); // GroupConclusionID
    public readonly static ConcurrentDictionary<uint, InferencePageExcelConfig> InferencePageDataMap = new(); // PageID
    public readonly static ConcurrentDictionary<uint, InferenceWordExcelConfig> InferenceWordDataMap = new(); // EntryID
    public readonly static ConcurrentDictionary<uint, InstableSprayBuffExcelConfig> InstableSprayBuffDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, InstableSprayDifficultyExcelConfig> InstableSprayDifficultyDataMap = new(); // DifficultyLevel
    public readonly static ConcurrentDictionary<uint, InstableSprayGachaExcelConfig> InstableSprayGachaDataMap = new(); // IntraPhase
    public readonly static ConcurrentDictionary<uint, InstableSprayLevelExcelConfig> InstableSprayLevelDataMap = new(); // LevelID
    public readonly static ConcurrentDictionary<uint, InstableSprayOverallExcelConfig> InstableSprayOverallDataMap = new(); // ActivityID
    public readonly static ConcurrentDictionary<uint, InstableSprayStageExcelConfig> InstableSprayStageDataMap = new(); // ActivityStage
    public readonly static ConcurrentDictionary<uint, InvestigationConfig> InvestigationDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, InvestigationDungeonConfig> InvestigationDungeonDataMap = new(); // entranceId
    public readonly static ConcurrentDictionary<uint, InvestigationMonsterConfig> InvestigationMonsterDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, InvestigationTargetConfig> InvestigationTargetDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, IrodoriChessCardExcelConfig> IrodoriChessCardDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, IrodoriChessDisorderExcelConfig> IrodoriChessDisorderDataMap = new(); // disorderId
    public readonly static ConcurrentDictionary<uint, IrodoriChessGearExcelConfig> IrodoriChessGearDataMap = new(); // gearId
    public readonly static ConcurrentDictionary<uint, IrodoriChessLevelExcelConfig> IrodoriChessLevelDataMap = new(); // levelId
    public readonly static ConcurrentDictionary<uint, IrodoriChessMapExcelConfig> IrodoriChessMapDataMap = new(); // mapId
    public readonly static ConcurrentDictionary<uint, IrodoriChessMapPointExcelConfig> IrodoriChessMapPointDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, ItemConfig> ItemDataMap = new(); // id. ItemConfig is subclassed.
    #endregion
    #region L
    public readonly static ConcurrentDictionary<uint, LanV2FireworksChallengeDataExcelConfig> LanV2FireworksChallengeDataMap = new(); // challengeId
    public readonly static ConcurrentDictionary<uint, LanV2FireworksFactorDataExcelConfig> LanV2FireworksFactorDataMap = new(); // factorId
    public readonly static ConcurrentDictionary<uint, LanV2FireworksOverallDataExcelConfig> LanV2FireworksOverallDataMap = new(); // scheduleId
    public readonly static ConcurrentDictionary<uint, LanV2FireworksSkillDataExcelConfig> LanV2FireworksSkillDataMap = new(); // skillId
    public readonly static ConcurrentDictionary<uint, LanV2FireworksStageDataExcelConfig> LanV2FireworksStageDataMap = new(); // stageId
    public readonly static ConcurrentDictionary<uint, LanV2OverAllDataExcelConfig> LanV2OverAllDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, LanV2ProjectionLevelExcelConfig> LanV2ProjectionLevelMap = new(); // levelId
    public readonly static ConcurrentDictionary<int, LevelSuppressExcelConfig> LevelSuppressMap = new(); // level
    public readonly static ConcurrentDictionary<uint, LevelTagExcelConfig> LevelTagMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, LevelTagGroupsExcelConfig> LevelTagGroupsMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, LevelTagMapAreaConfig> LevelTagMapAreaMap = new(); // LevelTagID
    public readonly static ConcurrentDictionary<uint, LevelTagResetExcelConfig> LevelTagResetMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, LoadingSituationExcelConfig> LoadingSituationMap = new(); // stageID
    public readonly static ConcurrentDictionary<uint, LoadingTipsExcelConfig> LoadingTipsMap = new(); // id
    public readonly static ConcurrentDictionary<string, LockTemplateExcelConfig> LockTemplateMap = new(); // type
    public readonly static ConcurrentDictionary<uint, LunaRiteBattleExcelConfig> LunaRiteBattleMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, LunaRiteBattleBuffExcelConfig> LunaRiteBattleBuffMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, LunaRitePreviewExcelConfig> LunaRitePreviewDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, LunaRiteSearchingData> LunaRiteSearchingMap = new(); // Id
    #endregion
    #region M
    public readonly static ConcurrentDictionary<uint, ConfigMainQuestScheme> MainQuestDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, MaterialCodexExcelConfig> MaterialCodexDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, MonsterCurveData> MonsterCurveDataMap = new(); // level
    public readonly static ConcurrentDictionary<uint, MonsterExcelConfig> MonsterDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, MonsterDescribeData> MonsterDescribeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, MusicGameBasicConfig> MusicGameBasicDataMap = new(); // id
    #endregion
    #region N
    public readonly static ConcurrentDictionary<uint, NewActivityExcelConfig> NewActivityDataMap = new(); // activityId
    public readonly static ConcurrentDictionary<uint, NewActivityWatcherConfig> NewActivityWatcherDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, NpcExcelConfig> NpcDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, NpcFirstMetExcelConfig> NpcFirstMetMap = new(); // id
    #endregion
    #region O
    public readonly static ConcurrentDictionary<uint, OpenStateData> OpenStateDataMap = new(); // id
    #endregion
    #region P
    public readonly static ConcurrentDictionary<uint, PersonalLineExcelConfig> PersonalLineDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, PlayerLevelExcelConfig> PlayerLevelDataMap = new(); // level
    #endregion
    #region Q
    public readonly static ConcurrentDictionary<uint, QuestCodexExcelConfig> QuestCodexDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, QuestData> QuestDataMap = new(); //subId
    public readonly static ConcurrentDictionary<uint, QuestGlobalVarConfig> QuestGlobalVarConfigMap = new(); //id
    public readonly static ConcurrentDictionary<uint, QuestResCollectionExcelConfig> QuestResCollectionExcelConfigMap = new(); //id
    public readonly static ConcurrentDictionary<uint, QuestSpecialShowConfig> QuestSpecialShowConfigMap = new(); //id
    #endregion
    #region R
    public readonly static ConcurrentDictionary<uint, RandTaskExcelConfig> RandTaskDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, RandTaskLevelConfig> RandTaskLevelDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, RandTaskRewardConfig> RandTaskRewardDataMap = new(); // ID
    public readonly static ConcurrentDictionary<ElementReactionType, ReactionEnergyExcelConfig> ReactionEnergyDataMap = new(); // type
    public readonly static ConcurrentDictionary<uint, RefreshPolicyExcelConfig> RefreshPolicyDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RegionSearchCondExcelConfig> RegionSearchCondDataMap = new(); // id
    public readonly static ConcurrentDictionary<string, ConfigTalentMixin[]> RelicAffixConfigDataMap = new(); // openConfig
    public readonly static ConcurrentDictionary<uint, ReliquaryAffixData> ReliquaryAffixDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ReliquaryCodexExcelConfig> ReliquaryCodexDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, ReliquaryDecomposeExcelConfig> ReliquaryDecomposeDataMap = new(); // Id
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, ReliquaryLevelData> ReliquaryLevelDataMap = new(); // <rank, level>
    public readonly static ConcurrentDictionary<uint, ReliquaryMainPropData> ReliquaryMainPropDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ReliquaryPowerupExcelConfig> ReliquaryPowerupDataMap = new(); // powerupMultiple
    public readonly static ConcurrentDictionary<uint, ReliquarySetData> ReliquarySetDataMap = new(); // setid
    public readonly static ConcurrentDictionary<uint, ReminderExcelConfig> ReminderDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ReminderIndexExcelConfig> ReminderIndexDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ReputationCityExcelConfig> ReputationCityDataMap = new(); // cityId
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, ReputationEntranceExcelConfig> ReputationEntranceMap = new(); // <textId, cityId>
    public readonly static ConcurrentDictionary<uint, ReputationExploreExcelConfig> ReputationExploreMap = new(); // exploreId
    public readonly static ConcurrentDictionary<uint, ReputationLevelExcelConfig> ReputationLevelMap = new(); // exploreId
    public readonly static ConcurrentDictionary<uint, ReputationQuestExcelConfig> ReputationQuestMap = new(); // ParentQuestId
    public readonly static ConcurrentDictionary<uint, ReputationRequestExcelConfig> ReputationRequestMap = new(); // RequestId
    public readonly static ConcurrentDictionary<uint, ReunionMissionExcelConfig> ReunionMissionDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ReunionPrivilegeExcelConfig> ReunionPrivilegeDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ReunionScheduleExcelConfig> ReunionScheduleDataMap = new(); // id
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, ReunionSignInExcelConfig> ReunionSignInDataMap = new(); // <id, dayIndex>
    public readonly static ConcurrentDictionary<uint, ReunionWatcherExcelConfig> ReunionWatcherDataMap = new(); // watcherGroupId
    public readonly static ConcurrentDictionary<uint, ReviseLevelGrowExcelConfig> ReviseLevelGrowDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RewardData> RewardDataMap = new(); // rewardId
    public readonly static ConcurrentDictionary<uint, RogueCellWeightExcelConfig> RogueCellWeightDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RogueDiaryBuffDataExcelConfig> RogueDiaryBuffDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RogueDiaryCardWeightExcelConfig> RogueDiaryCardWeightDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RogueDiaryDungeonExcelConfig> RogueDiaryDungeonDataMap = new(); // dungeonId
    public readonly static ConcurrentDictionary<uint, RogueDiaryPreviewExcelConfig> RogueDiaryPreviewDataMap = new(); // activityId
    public readonly static ConcurrentDictionary<uint, RogueDiaryResourceExcelConfig> RogueDiaryResourceDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RogueDungeonCellExcelConfig> RogueDungeonCellDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RogueGadgetRotConfig> RogueGadgetRotMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RogueMonsterPoolExcelConfig> RogueMonsterPoolDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RogueSequenceExcelConfig> RogueSequenceDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RogueStageExcelConfig> RogueStageDataMap = new(); // stageId
    public readonly static ConcurrentDictionary<uint, RogueTokenExcelConfig> RogueTokenDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, RoomExcelConfig> RoomDataMap = new(); // id
    #endregion
    #region S
    public readonly static ConcurrentDictionary<uint, SceneData> SceneDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ConfigLevelNpcBornPos> SceneNpcBornDataMap = new(); // sceneId
    public readonly static ConcurrentDictionary<string, ScenePointData> ScenePointDataMap = new(); // filename
    public readonly static ConcurrentDictionary<uint, SceneTagData> SceneTagDataMap = new(); // id
    public readonly static ConcurrentDictionary<uint, ShopData> ShopDataMap = new(); // shopId	
    public readonly static ConcurrentDictionary<uint, ShopGoodsData> ShopGoodsDataMap = new(); // goodsId
    #endregion
    #region T
    public readonly static ConcurrentDictionary<string, ConfigTalentMixin[]> TeamResonanceConfigDataMap = new(); // openConfig	
    public readonly static ConcurrentDictionary<uint, TeamResonanceData> TeamResonanceDataMap = new(); // teamResonanceId 
    public readonly static ConcurrentDictionary<uint, TowerFloorExcelConfig> TowerFloorDataMap = new(); // floorId
    public readonly static ConcurrentDictionary<uint, TowerLevelExcelConfig> TowerLevelDataMap = new(); // levelId
    public readonly static ConcurrentDictionary<uint, TowerScheduleExcelConfig> TowerScheduleDataMap = new(); // scheduleId
    public readonly static ConcurrentDictionary<uint, TriggerExcelConfig> TriggerDataMap = new(); // id
    #endregion
    #region U
    #endregion
    #region V
    #endregion
    #region W
    public readonly static ConcurrentDictionary<string, ConfigTalentMixin[]> WeaponAffixConfigDataMap = new(); // openConfig
    public readonly static ConcurrentDictionary<uint, WeaponCodexExcelConfig> WeaponCodexDataMap = new(); // Id
    public readonly static ConcurrentDictionary<uint, WeaponCurveData> WeaponCurveDataMap = new(); // level
    public readonly static ConcurrentDictionary<Tuple<uint, uint>, WeaponPromoteData> WeaponPromoteDataMap = new(); // <weaponPromoteId, promoteLevel>
    public readonly static ConcurrentDictionary<uint, WeaponLevelData> WeaponLevelDataMap = new();  // level
    public readonly static ConcurrentDictionary<uint, WeatherData> WeatherDataMap = new(); // areaId
    public readonly static ConcurrentDictionary<uint, WorldAreaConfig> WorldAreaDataMap = new(); // ID
    public readonly static ConcurrentDictionary<uint, WorldLevelExcelConfig> WorldLevelDataMap = new(); // level
    #endregion

    private readonly static Dictionary<uint, SceneInfo> SceneScripts = new(); //sceneId

    public static readonly JsonSerializer Serializer = new()
    {
        // To handle $type
        TypeNameHandling = TypeNameHandling.Objects,
        MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead,
        SerializationBinder = new KnownTypesBinder(),
    };
    public static SceneInfo? GetSceneScriptsAsync(uint sceneId)
    {
        if (SceneScripts.TryGetValue(sceneId, out SceneInfo? value))
            return value;

        if (LoadSceneScripts(sceneId, GameServer.Configuration.structure.Scripts))
            return SceneScripts[sceneId];

        return null;
    }
    private static bool LoadSceneScripts(uint sceneId, string scriptPath)
    {
        SceneInfo? scene = SceneInfo.Create(sceneId, scriptPath);
        if (scene == null)
            return false;

        SceneScripts.Add(sceneId, scene);
        return true;
    }
    private static async Task LoadConfigAbility(string path, IDictionary<string, ConfigAbility> map)
    {
        await Task.Yield();
        string[] filePaths = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
        filePaths.AsParallel().ForAll(async file =>
        {
            FileInfo? filePath = new(file);
            if (filePath.Name == "ConfigAbility_Scene_Test_WhiteBox.json") return; //malformed
            using StringReader? sr = new(await File.ReadAllTextAsync(filePath.FullName));
            using JsonTextReader? jr = new(sr);

            ConfigAbilityContainer[] fileData = Serializer.Deserialize<ConfigAbilityContainer[]>(jr);

            foreach (ConfigAbilityContainer container in fileData)
            {
                map.Add(container.Default.abilityName, container.Default);
                await container.Default.Initialize();
            }
        });
    }
    private static async Task LoadBinOutFolder<Obj, Key>(string path, Func<Obj, Key> keySelector, IDictionary<Key, Obj> map) where Key : notnull
    {
        await Task.Yield();
        string[] filePaths = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
        List<Task>? tasks = new();
        filePaths.AsParallel().ForAll(async file =>
        {
            FileInfo? filePath = new(file);
            using StringReader? sr = new(await File.ReadAllTextAsync(filePath.FullName));
            string fileName = filePath.Name;
            using JsonTextReader? jr = new(sr);

            Obj? fileData = Serializer.Deserialize<Obj>(jr);

            Key key = keySelector(fileData);
            map.Add(key, fileData);
        });
    }
    private static async Task LoadBinOutFolder<Obj>(string path, IDictionary<string, Obj> map, bool isDictionaryJson = true)
    {
        await Task.Yield();
        string[] filePaths = Directory.GetFiles(path, "*.json", SearchOption.TopDirectoryOnly);
        filePaths.AsParallel().ForAll(async file =>
        {
            FileInfo? filePath = new(file);
            using StringReader? sr = new(await File.ReadAllTextAsync(filePath.FullName));
            using JsonTextReader? jr = new(sr);

            if (isDictionaryJson)
            {
                Dictionary<string, Obj>? fileData = Serializer.Deserialize<Dictionary<string, Obj>>(jr);
                foreach (KeyValuePair<string, Obj> data in fileData)
                {
                    map.Add(data.Key, data.Value);
                }
            }
            else
            {
                try
                {
                    Obj? fileData = Serializer.Deserialize<Obj>(jr);
                    // Use the name (without ".json") of the file as the key
                    map.Add(Regex.Replace(filePath.Name, "\\.json", ""), fileData);
                }
                catch (Exception e)
                {
                    Logger.WriteErrorLine(filePath.FullName);
                    Logger.WriteErrorLine(e.Message);
                }
            }
        });
    }
    // To handle derived types
    private static async Task LoadExcel<Obj, Key, DerivedType>(string path, Func<Obj, Key> keySelector, IDictionary<Key, Obj> map) where Key : notnull where DerivedType : class, Obj, new()
    {
        string[] lines = await File.ReadAllLinesAsync(path);
        //skip header
        Parallel.For(1, lines.Length, (i) =>
        {
            if (string.IsNullOrEmpty(lines[i])) return;
            DerivedType obj = TsvParser.DeserializeString<DerivedType>(lines[i]);
            map.Add(keySelector(obj), obj);
        });
    }
    private static async Task LoadExcel<Obj, Key>(string path, Func<Obj, Key> keySelector, IDictionary<Key, Obj> map) where Key : notnull where Obj : class, new()
    {
        string[] lines = await File.ReadAllLinesAsync(path);
        //skip header
        Parallel.For(1, lines.Length, (i) =>
        {
            if (string.IsNullOrEmpty(lines[i])) return;
            Obj obj = TsvParser.DeserializeString<Obj>(lines[i]);
            map.Add(keySelector(obj), obj);
        });
    }
    private static async Task LoadDungeonLevelEntityConfig(string path, IDictionary<uint, IList<string>> map)
    {
        string[] lines = await File.ReadAllLinesAsync(path);
        //skip header
        for (int i = 1; i < lines.Length; i++)
        {
            if (string.IsNullOrEmpty(lines[i])) return;
            DungeonLevelEntityConfig obj = TsvParser.DeserializeString<DungeonLevelEntityConfig>(lines[i]);
            if (!map.ContainsKey(obj.id)) map[obj.id] = new List<string>();
            map[obj.id].Add(obj.levelConfigName);
        };
    }

    public static async Task LoadAllResourcesAsync(string resourcesPath)
    {
        Stopwatch watch = Stopwatch.StartNew();
        Logger.WriteLine("Loading Resources...");
        string excelPath = Path.Combine(resourcesPath, "txt/");
        string binPath = Path.Combine(resourcesPath, "json/");

        string file = Path.Combine(binPath, "Common", "ConfigGlobalCombat.json");
        FileInfo fi = new(file);
        using StringReader sr = new(await File.ReadAllTextAsync(fi.FullName));
        using JsonTextReader jr = new(sr);
        GlobalCombatData = Serializer.Deserialize<ConfigGlobalCombat>(jr);

        await Task.WhenAll(new Task[]
        {
			#region A
			LoadExcel(Path.Combine(excelPath, "AbilityOverride_Avatar.txt"), o => o.id, AbilityOverrideDataMap),
            LoadExcel(Path.Combine(excelPath, "AbilityOverride_Level.txt"), o => o.id, AbilityOverrideDataMap),
            LoadExcel(Path.Combine(excelPath, "AbilityOverride_Monster.txt"), o => o.id, AbilityOverrideDataMap),
            LoadExcel(Path.Combine(excelPath, "AbilityPropData.txt"), o => o.propName, AbilityPropDataMap),
            LoadExcel(Path.Combine(excelPath, "AbilityStateResistanceByID.txt"), o => o.id, AbilityStateResistanceByIDMap),
            LoadExcel(Path.Combine(excelPath, "AchievementData.txt"), o => o.id, AchievementDataMap),
            LoadExcel(Path.Combine(excelPath, "AchievementGoalData.txt"), o => o.orderId, AchievementGoalDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityAbilityGroup.txt"), o => o.index, ActivityAbilityGroupMap),
            LoadExcel(Path.Combine(excelPath, "ArenaChallengeLevelData.txt"), o => Tuple.Create(o.ID, o.arenaChallengeId), ActivityArenaChallengeDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityBannerData.txt"), o => o.activityId, ActivityBannerDataMap),
            LoadExcel(Path.Combine(excelPath, "ChessAffixData.txt"), o => o.ID, ActivityChessAffixDataMap),
            LoadExcel(Path.Combine(excelPath, "ChessCardData.txt"), o => o.ID, ActivityChessCardDataMap),
            LoadExcel(Path.Combine(excelPath, "ChessGearData.txt"), o => o.gearID, ActivityChessGearDataMap),
            LoadExcel(Path.Combine(excelPath, "ChessLevelData.txt"), o => o.level, ActivityChessLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "ChessMapData.txt"), o => o.chessMapID, ActivityChessMapDataMap),
            LoadExcel(Path.Combine(excelPath, "ChessMapPointData.txt"), o => o.ID, ActivityChessMapPointDataMap),
            LoadExcel(Path.Combine(excelPath, "ChessPreviewData.txt"), o => o.activityID, ActivityChessPreviewDataMap),
            LoadExcel(Path.Combine(excelPath, "ChessScheduleData.txt"), o => o.day, ActivityChessScheduleDataMap),
            LoadExcel(Path.Combine(excelPath, "CrystalLinkConditionBuff.txt"), o => o.buffId, ActivityCrystalLinkCondBuffMap),
            LoadExcel(Path.Combine(excelPath, "CrystalLinkDifficultyData.txt"), o => o.difficultyId, ActivityCrystalLinkDifficultyDataMap),
            LoadExcel(Path.Combine(excelPath, "CrystalLinkEffectBuff.txt"), o => o.buffId, ActivityCrystalLinkEffectBuffMap),
            LoadExcel(Path.Combine(excelPath, "CrystalLinkLevelData.txt"), o => o.levelId, ActivityCrystalLinkLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityCusDungeon.txt"), o => o.dungeonID, CustomLevelDungeonDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityDeliveryDailyData.txt"), o => o.dailyConfigId, ActivityDeliveryDailyDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityDeliveryData.txt"), o => o.scheduleId, ActivityDeliveryDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityDeliveryWatcherData.txt"), o => o.id, ActivityDeliveryWatcherDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGachaBaseData.txt"), o => o.activityId, ActivityGachaBaseDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGachaRobotData.txt"), o => o.robotId, ActivityGachaRobotDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGachaStageData.txt"), o => o.stageId, ActivityGachaStageDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGachaTargetData.txt"), o => o.Id, ActivityGachaTargetDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGearData.txt"), o => o.id, ActivityGearDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGearGadgetGearData.txt"), o => o.id, ActivityGearGadgetGearDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGearGadgetShaftData.txt"), o => o.id, ActivityGearGadgetShaftDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGearJigsawData.txt"), o => o.id, ActivityGearJigsawDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGearLevelData.txt"), o => o.id, ActivityGearLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityGroupLinksData.txt"), o => o.linkId, ActivityGroupLinksDataMap), //empty...
			LoadExcel(Path.Combine(excelPath, "ActivityHachiFinalStageData.txt"), o => o.Id, ActivityHachiFinalStageDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityHachiStageData.txt"), o => o.Id, ActivityHachiStageDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityHideandSeekData.txt"), o => o.id, ActivityHideAndSeekDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityJigsawDetailData.txt"), o => o.PuzzleID, ActivityJigsawDetailDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityPhotographData.txt"), o => o.id, ActivityPhotographDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityPhotographPosData.txt"), o => o.id, ActivityPhotographPosDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityPlantFlowerDaily.txt"), o => o.dailyConfigId, ActivityPlantFlowerDailyMap),
            LoadExcel(Path.Combine(excelPath, "ActivityPlantFlowerMain.txt"), o => o.scheduleId, ActivityPlantFlowerMainMap),
            LoadExcel(Path.Combine(excelPath, "ActivityRSLogicData.txt"), o => o.id, RegionSearchCondDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityShopOverallData.txt"), o => o.scheduleId, ActivityShopOverallDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivityShopSheetData.txt"), o => o.id, ActivityShopSheetDataMap),
            LoadExcel(Path.Combine(excelPath, "ActivitySkillData.txt"), o => o.id, ActivitySkillDataMap),
            LoadExcel(Path.Combine(excelPath, "AnimalCodexData.txt"), o => o.Id, AnimalCodexDataMap),
            LoadExcel(Path.Combine(excelPath, "AnimalDescribeData.txt"), o => o.id, AnimalDescribeDataMap),
            LoadExcel(Path.Combine(excelPath, "AranaraCollectionData.txt"), o => o.CollectionID, AranaraCollectionDataMap),
            LoadExcel(Path.Combine(excelPath, "AsterActivityPreviewData.txt"), o => o.activityId, AsterActivityPreviewDataMap),
            LoadExcel(Path.Combine(excelPath, "AsterLittleData.txt"), o => o.stage_id, AsterLittleDataMap),
            LoadExcel(Path.Combine(excelPath, "AsterMidDifficultyData.txt"), o => o.id, AsterMidDifficultyDataMap),
            LoadExcel(Path.Combine(excelPath, "AsterMidData.txt"), o => o.id, AsterMidDataMap),
            LoadExcel(Path.Combine(excelPath, "AsterMidGroupsData.txt"), o => o.id, AsterMidGroupsDataMap),
            LoadExcel(Path.Combine(excelPath, "AsterMissionData.txt"), o => o.missionId, AsterMissionDataMap),
            LoadExcel(Path.Combine(excelPath, "AsterStageData.txt"), o => o.id, AsterStageDataMap),
            LoadExcel(Path.Combine(excelPath, "AttackAttenuationData.txt"), o => o.group, AttackAttenuationDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarCardChangeData.txt"), o => o.AvatarID, AvatarCardChangeDataMap),
            LoadExcel(Path.Combine(excelPath, "AvartarCodexData.txt"), o => o.sortId, AvatarCodexDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarCostumeData.txt"), o => o.skinId, AvatarCostumeDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarCurveData.txt"), o => o.level, AvatarCurveDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarData.txt"), o => o.id, AvatarDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarExtraPropertyData.txt"), o => o.ID, AvatarExtraPropertyDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarFettersLevelData.txt"), o => o.fetterLevel, AvatarFetterLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarFlycloakData.txt"), o => o.flycloakId, AvatarFlycloakDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarHeroEntityData.txt"), o => o.avatarId, AvatarHeroEntityDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarLevelData.txt"), o => o.Level, AvatarLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarPointData.txt"), o => Tuple.Create(o.Star, o.Level), AvatarPointDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarPromoteData.txt"), o => Tuple.Create(o.avatarPromoteId, o.promoteLevel), AvatarPromoteDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarReplaceCostumeData.txt"), o => o.avatarId, AvatarReplaceCostumeDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarSkillData.txt"), o => o.id, AvatarSkillDataMap),
            LoadExcel(Path.Combine(excelPath, "AvatarSkillDepotData.txt"), o => o.id, AvatarSkillDepotDataMap),
            LoadExcel(Path.Combine(excelPath, "TalentSkillData.txt"), o => o.talentId, AvatarTalentDataMap),
			#endregion
			#region B
			LoadExcel(Path.Combine(excelPath, "BargainData.txt"), o => o.id, BargainDataMap),
            LoadExcel(Path.Combine(excelPath, "BartenderAffixData.txt"), o => o.id, BartenderAffixDataMap),
            LoadExcel(Path.Combine(excelPath, "BartenderBasisData.txt"), o => o.activityId, BartenderBasisDataMap),
            LoadExcel(Path.Combine(excelPath, "BartenderEventData.txt"), o => Tuple.Create(o.effectType, o.miscId, o.effectName), BartenderEventDataMap),
            LoadExcel(Path.Combine(excelPath, "BartenderFormulaData.txt"), o => o.id, BartenderFormulaDataMap),
            LoadExcel(Path.Combine(excelPath, "BartenderOrderData.txt"), o => o.id, BartenderOrderDataMap),
            LoadExcel(Path.Combine(excelPath, "BartenderTaskData.txt"), o => o.id, BartenderTaskDataMap),
            LoadExcel(Path.Combine(excelPath, "BartenderTaskOrderData.txt"), o => o.questId, BartenderTaskOrderDataMap),
            LoadExcel(Path.Combine(excelPath, "BattlePassLevel.txt"), o => o.level, BattlePassLevelMap),
            LoadExcel(Path.Combine(excelPath, "BattlePassMission.txt"), o => o.id, BattlePassMissionMap),
            LoadExcel(Path.Combine(excelPath, "BattlePassReward.txt"), o => Tuple.Create(o.indexId, o.level), BattlePassRewardMap),
            LoadExcel(Path.Combine(excelPath, "BattlePassSchedule.txt"), o => o.id, BattlePassScheduleMap),
            LoadExcel(Path.Combine(excelPath, "BirthdayMailData.txt"), o => o.id, BirthdayMailDataMap),
            LoadExcel(Path.Combine(excelPath, "BlessingScanData.txt"), o => o.id, BlessingScanDataMap),
            LoadExcel(Path.Combine(excelPath, "BlessingScanDropData.txt"), o => o.id, BlessingScanDropDataMap),
            LoadExcel(Path.Combine(excelPath, "BlessingScanPicData.txt"), o => o.id, BlessingScanPicDataMap),
            LoadExcel(Path.Combine(excelPath, "BlessingScanTypeData.txt"), o => o.id, BlessingScanTypeDataMap),
            LoadExcel(Path.Combine(excelPath, "BlitzRushOverAll.txt"), o => o.id, BlitzRushOverAllMap),
            LoadExcel(Path.Combine(excelPath, "BlitzRushParkour.txt"), o => o.id, BlitzRushParkourMap),
            LoadExcel(Path.Combine(excelPath, "BlitzRushStage.txt"), o => o.id, BlitzRushStageMap),
            LoadExcel(Path.Combine(excelPath, "BlossomChestData.txt"), o => o.id, BlossomChestDataMap),
            LoadExcel(Path.Combine(excelPath, "BlossomGroupsData.txt"), o => o.id, BlossomGroupsDataMap),
            LoadExcel(Path.Combine(excelPath, "BlossomOpenData.txt"), o => o.cityId, BlossomOpenDataMap),
            LoadExcel(Path.Combine(excelPath, "BlossomRefreshData.txt"), o => o.id, BlossomRefreshDataMap),
            LoadExcel(Path.Combine(excelPath, "BlossomReviseData.txt"), o => o.id, BlossomReviseDataMap),
            LoadExcel(Path.Combine(excelPath, "BlossomSectionOrderData.txt"), o => o.id, BlossomSectionOrderDataMap),
            LoadExcel(Path.Combine(excelPath, "BlossomTalkData.txt"), o => o.id, BlossomTalkDataMap),
            LoadExcel(Path.Combine(excelPath, "BonusActivityData.txt"), o => o.id, BonusActivityDataMap),
            LoadExcel(Path.Combine(excelPath, "BonusTreasureSolutionData.txt"), o => o.id, BonusTreasureSolutionDataMap),
            LoadExcel(Path.Combine(excelPath, "BooksCodexData.txt"), o => o.Id, BooksCodexDataMap),
            LoadExcel(Path.Combine(excelPath, "BoredActionPriority.txt"), o => o.action_type, BoredActionPriorityMap),
            LoadExcel(Path.Combine(excelPath, "BoredCreateMonster.txt"), o => o.player_level, BoredCreateMonsterMap),
            LoadExcel(Path.Combine(excelPath, "BoredEvent.txt"), o => o.event_type, BoredEventMap),
            LoadExcel(Path.Combine(excelPath, "BoredMonsterPool.txt"), o => o.id, BoredMonsterPoolMap),
            LoadExcel(Path.Combine(excelPath, "BounceConjuringChapterData.txt"), o => o.id, BounceConjuringChapterDataMap),
            LoadExcel(Path.Combine(excelPath, "BounceConjuringPreviewData.txt"), o => o.id, BounceConjuringPreviewDataMap),
            LoadExcel(Path.Combine(excelPath, "BuffData.txt"), o => o.serverBuffId, BuffDataMap),
            LoadExcel(Path.Combine(excelPath, "BuoyantCombatData.txt"), o => o.id, BuoyantCombatDataMap),
            LoadExcel(Path.Combine(excelPath, "BuoyantCombatLevelData.txt"), o => o.id, BuoyantCombatLevelDataMap),
			#endregion
			#region C
			LoadExcel(Path.Combine(excelPath, "CaptureData.txt"), o => o.monsterID, CaptureDataMap),
            LoadExcel(Path.Combine(excelPath, "CaptureTagsData.txt"), o => o.captureTagID, CaptureTagsDataMap),
            LoadExcel(Path.Combine(excelPath, "CatalogData.txt"), o => o.catalogID, CatalogDataMap),
            LoadExcel(Path.Combine(excelPath, "ChapterData.txt"), o => o.id, ChapterDataMap),
            LoadExcel(Path.Combine(excelPath, "CharAmusementLevelData.txt"), o => o.stage_id, CharAmusementLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "CharAmusementOverallData.txt"), o => o.scheduleID, CharAmusementOverallDataMap),
            LoadExcel(Path.Combine(excelPath, "CharAmusementStageData.txt"), o => o.activity_stage, CharAmusementStageDataMap),
            LoadExcel(Path.Combine(excelPath, "ChargeBarStyleData.txt"), o => o.id, ChargeBarStyleDataMap),
            LoadExcel(Path.Combine(excelPath, "ChestDropData.txt"), o => Tuple.Create(o.minimum_level, o.dropID), ChestDropDataMap),
            LoadExcel(Path.Combine(excelPath, "CityData.txt"), o => o.cityId, CityDataMap),
            LoadExcel(Path.Combine(excelPath, "CityLevelupData.txt"), o => Tuple.Create(o.city_id, o.level), CityLevelupDataMap),
            LoadExcel(Path.Combine(excelPath, "CityTaskOpenData.txt"), o => o.cityId, CityTaskOpenDataMap),
            LoadExcel(Path.Combine(excelPath, "CombineData.txt"), o => o.combineId, CombineDataMap),
            LoadExcel(Path.Combine(excelPath, "ComponentData.txt"), o => o.ComponentID, ComponentDataMap),
            LoadExcel(Path.Combine(excelPath, "CompoundBoostData.txt"), o => o.materialID, CompoundBoostDataMap),
            LoadExcel(Path.Combine(excelPath, "CompoundData.txt"), o => o.id, CompoundDataMap),
            LoadExcel(Path.Combine(excelPath, "ConstValueActivityData.txt"), o => o.name, ConstValueDataMap),
            LoadExcel(Path.Combine(excelPath, "ConstValueData.txt"), o => o.name, ConstValueDataMap),
            LoadExcel(Path.Combine(excelPath, "ConstValueGcgData.txt"), o => o.name, ConstValueDataMap),
            LoadExcel(Path.Combine(excelPath, "ConstValueHomeworldData.txt"), o => o.name, ConstValueDataMap),
            LoadExcel(Path.Combine(excelPath, "ConvertData.txt"), o => o.ConvertID, ConvertDataMap),
            LoadExcel(Path.Combine(excelPath, "ConvertHomeWorldData.txt"), o => o.ConvertID, ConvertDataMap),
            LoadExcel(Path.Combine(excelPath, "CookBonusData.txt"), o => o.recipeId, CookBonusDataMap),
            LoadExcel(Path.Combine(excelPath, "CookRecipeData.txt"), o => o.id, CookRecipeDataMap),
            LoadExcel(Path.Combine(excelPath, "CoopCGData.txt"), o => o.id, CoopCGDataMap),
            LoadExcel(Path.Combine(excelPath, "CoopChapterData.txt"), o => o.id, CoopChapterDataMap),
            LoadExcel(Path.Combine(excelPath, "CoopInteractionData.txt"), o => o.id, CoopInteractionDataMap),
            LoadExcel(Path.Combine(excelPath, "CoopInteractionData_Exported.txt"), o => o.id, CoopInteractionDataMap),
            LoadExcel(Path.Combine(excelPath, "CoopPointData.txt"), o => o.id, CoopPointDataMap),
            LoadExcel(Path.Combine(excelPath, "CoopRewardData.txt"), o => o.id, CoopRewardDataMap),
            LoadExcel(Path.Combine(excelPath, "CusLevelDungeonData.txt"), o => o.dungeonID, CustomLevelDungeonDataMap),
            LoadExcel(Path.Combine(excelPath, "CusLevelGroupData.txt"), o => o.groupId, CustomLevelGroupDataMap),
            LoadExcel(Path.Combine(excelPath, "CusLevelTagData.txt"), o => o.configId, CustomLevelTagDataMap),
            LoadExcel(Path.Combine(excelPath, "CustomGadgetConfigIdData.txt"), o => o.configId, CustomGadgetConfigIdDataMap),
            LoadExcel(Path.Combine(excelPath, "CustomGadgetIdData_Level.txt"), o => o.configId, CustomGadgetConfigIdDataMap),
            LoadExcel(Path.Combine(excelPath, "CustomGadgetLevelTagData.txt"), o => o.ID, CustomGadgetSlotLevelTagDataMap),
            LoadExcel(Path.Combine(excelPath, "CustomGadgetRootData.txt"), o => o.rootGadgetID, CustomGadgetRootDataMap),
            LoadExcel(Path.Combine(excelPath, "CustomGadgetRootData_Level.txt"), o => o.rootGadgetID, CustomGadgetRootDataMap),
            LoadExcel(Path.Combine(excelPath, "CustomGadgetSlotData.txt"), o => o.slotID, CustomGadgetSlotDataMap),
            LoadExcel(Path.Combine(excelPath, "CustomGadgetSlotData_Level.txt"), o => o.slotID, CustomGadgetSlotDataMap),
            LoadExcel(Path.Combine(excelPath, "CutsceneData.txt"), o => o.id, CutsceneDataMap),
			#endregion
			#region D
			LoadExcel(Path.Combine(excelPath, "DailyDungeonData.txt"), o => o.id, DailyDungeonDataMap),
            LoadExcel(Path.Combine(excelPath, "DailyTaskData.txt"), o => o.ID, DailyTaskDataMap),
            LoadExcel(Path.Combine(excelPath, "DailyTaskLevelData.txt"), o => o.ID, DailyTaskLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "DailyTaskRewardData.txt"), o => o.ID, DailyTaskRewardDataMap),
            LoadExcel(Path.Combine(excelPath, "DeathRegionLevelData.txt"), o => o.DeathZoneLevel, DeathRegionLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "DeshretObeliskArgumentData.txt"), o => o.addedValueID, DeshretObeliskArgumentDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_Activity.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_Coop.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_Exported.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_LiyueIQ.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_LiyueIQ_2.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_LiyueIQ_3.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_LiyueLQ_Adult.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_LiyueMQ.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_LiyueWQ.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_MengdeIQ.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_MengdeIQ_2.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_MengdeLQ_Adult.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_MengdeLQ_Teen.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_MengdeMQ.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DialogData_NPC.txt"), o => o.id, DialogDataMap),
            LoadExcel(Path.Combine(excelPath, "DigGroupLinkData.txt"), o => o.id, DigGroupLinkDataMap),
            LoadExcel<ItemConfig, uint, DisplayItemData>(Path.Combine(excelPath, "DisplayItemData.txt"), o => o.id, ItemDataMap),
            LoadExcel(Path.Combine(excelPath, "DocumentData.txt"), o => o.id, DocumentDataMap),
            LoadExcel(Path.Combine(excelPath, "DraftData.txt"), o => o.id, DraftDataMap),
            LoadExcel(Path.Combine(excelPath, "DragonSpineEnhanceData.txt"), o => o.id, DragonSpineEnhanceDataMap),
            LoadExcel(Path.Combine(excelPath, "DragonSpineMissionData.txt"), o => o.id, DragonSpineMissionDataMap),
            LoadExcel(Path.Combine(excelPath, "DragonSpinePreviewData.txt"), o => o.id, DragonSpinePreviewDataMap),
            LoadExcel(Path.Combine(excelPath, "DragonSpineStageData.txt"), o => o.id, DragonSpineStageDataMap),
            LoadExcel<BaseDropIndexConfig, uint, DropLeafExcelConfig>(Path.Combine(excelPath, "DropLeafData.txt"), o => o.dropId, DropIndexDataMap),
            LoadExcel(Path.Combine(excelPath, "DropSubfieldData.txt"), o => o.branchDropPoolId, DropSubfieldDataMap),
            LoadExcel<BaseDropIndexConfig, uint, DropTreeExcelConfig>(Path.Combine(excelPath, "DropTreeData.txt"), o => o.dropId, DropIndexDataMap),
            LoadExcel(Path.Combine(excelPath, "DungeonChallengeData.txt"), o => o.id, DungeonChallengeDataMap),
            LoadExcel(Path.Combine(excelPath, "DungeonData.txt"), o => o.id, DungeonDataMap),
            LoadExcel(Path.Combine(excelPath, "DungeonElementChallengeData.txt"), o => o.dungeonId, DungeonElementChallengeDataMap),
            LoadExcel(Path.Combine(excelPath, "DungeonEntryData.txt"), o => o.id, DungeonEntryDataMap),
            LoadDungeonLevelEntityConfig(Path.Combine(excelPath, "DungeonLevelEntityData.txt"), DungeonLevelEntityDataMap),
            LoadExcel(Path.Combine(excelPath, "DungeonMapArea.txt"), o => o.dungeonID, DungeonMapAreaMap),
            LoadExcel(Path.Combine(excelPath, "DungeonPassData.txt"), o => o.id, DungeonPassDataMap),
            LoadExcel(Path.Combine(excelPath, "DungeonReminderData.txt"), o => o.id, ReminderDataMap),
            LoadExcel(Path.Combine(excelPath, "DungeonRosterData.txt"), o => o.id, DungeonRosterDataMap),
            LoadExcel(Path.Combine(excelPath, "DungeonSerialData.txt"), o => o.id, DungeonSerialDataMap),
			#endregion
			#region E
			LoadExcel(Path.Combine(excelPath, "EchoShellFloatSignal.txt"), o => o.id, EchoShellFloatSignalMap),
            LoadExcel(Path.Combine(excelPath, "EchoShellPreview.txt"), o => o.id, EchoShellPreviewMap),
            LoadExcel(Path.Combine(excelPath, "EchoShellRewardData.txt"), o => o.id, EchoShellRewardDataMap),
            LoadExcel(Path.Combine(excelPath, "EchoShellSummerTimeDungeon.txt"), o => o.dungeonId, EchoShellSummerTimeDungeonMap),
            LoadExcel(Path.Combine(excelPath, "EchoShellVoiceData.txt"), o => o.id, EchoShellVoiceDataMap),
            LoadExcel(Path.Combine(excelPath, "EffigyChallengeData.txt"), o => o.id, EffigyChallengeDataMap),
            LoadExcel(Path.Combine(excelPath, "EffigyChallengeV2LevelData.txt"), o => o.ConfigID, EffigyChallengeV2DataMap),
            LoadExcel(Path.Combine(excelPath, "EffigyChallengeV2SkillData.txt"), o => o.id, EffigyChallengeV2SkillDataMap),
            LoadExcel(Path.Combine(excelPath, "ECV2OverallData.txt"), o => o.id, EffigyChallengeV2OverallDataMap),
            LoadExcel(Path.Combine(excelPath, "EffigyDifficultyData.txt"), o => o.id, EffigyDifficultyDataMap),
            LoadExcel(Path.Combine(excelPath, "ECV2DifficultyData.txt"), o => o.id, EffigyDifficultyV2DataMap),
            LoadExcel(Path.Combine(excelPath, "EffigyLimitingConditionData.txt"), o => o.id, EffigyLimitingConditionDataMap),
            LoadExcel(Path.Combine(excelPath, "EffigyRewardData.txt"), o => o.id, EffigyRewardDataMap),
            LoadExcel(Path.Combine(excelPath, "ElementData.txt"), o => o.level, ElementDataMap),
            LoadExcel(Path.Combine(excelPath, "ElementStateData.txt"), o => o.elementType, ElementStateDataMap),
            LoadExcel(Path.Combine(excelPath, "EndureTemplateData.txt"), o => o.type, EndureTemplateDataMap),
            LoadExcel(Path.Combine(excelPath, "EntityDropSubfieldData.txt"), o => o.EntityId, EntityDropSubfieldDataMap),
            LoadExcel(Path.Combine(excelPath, "EntityMutiPlayerFixData.txt"), o => o.id, EntityMultiPlayerFixDataMap),
            LoadExcel(Path.Combine(excelPath, "EnvAnimalGather.txt"), o => o.animalId, EnvAnimalGatherDataMap),
            LoadExcel(Path.Combine(excelPath, "EnvAnimalWeight.txt"), o => o.envType, EnvAnimalWeightDataMap),
            LoadExcel(Path.Combine(excelPath, "EquipAffixData.txt"), o => o.affixId, EquipAffixDataMap),
            LoadExcel(Path.Combine(excelPath, "ExclusiveRuleData.txt"), o => o.id, ExclusiveRuleDataMap),
            LoadExcel(Path.Combine(excelPath, "ExhibitionCardData.txt"), o => o.id, ExhibitionListDataMap),
            LoadExcel(Path.Combine(excelPath, "ExhibitionListData.txt"), o => o.id, ExhibitionListDataMap),
            LoadExcel(Path.Combine(excelPath, "ExhibitionDisplayData.txt"), o => o.DisplayID, ExhibitionDisplayDataMap),
            LoadExcel(Path.Combine(excelPath, "ExhibitionLuaStorageData.txt"), o => o.ID, ExhibitionLuaStorageDataMap),
            LoadExcel(Path.Combine(excelPath, "ExhibitionScoreData.txt"), o => o.id, ExhibitionScoreDataMap),
            LoadExcel(Path.Combine(excelPath, "ExhibitionSeriesData.txt"), o => o.SeriesID, ExhibitionSeriesDataMap),
            LoadExcel(Path.Combine(excelPath, "ExpeditionBonusData.txt"), o => o.id, ExpeditionBonusDataMap),
            LoadExcel(Path.Combine(excelPath, "ExpeditionChallengeData.txt"), o => o.id, ExpeditionChallengeDataMap),
            LoadExcel(Path.Combine(excelPath, "ExpeditionData.txt"), o => o.ID, ExpeditionDataMap),
            LoadExcel(Path.Combine(excelPath, "ExpeditionDifficultyData.txt"), o => o.id, ExpeditionDifficultyDataMap),
            LoadExcel(Path.Combine(excelPath, "ExpeditionMarkData.txt"), o => o.id, ExpeditionMarkDataMap),
            LoadExcel(Path.Combine(excelPath, "ExpeditionPathData.txt"), o => o.id, ExpeditionPathDataMap),
            LoadExcel(Path.Combine(excelPath, "ExpeditionPreviewDat.txt"), o => o.id, ExpeditionPreviewDataMap),
            LoadExcel(Path.Combine(excelPath, "AreaTotalExpData.txt"), o => o.areaID, ExploreAreaTotalExpDataMap),
            LoadExcel(Path.Combine(excelPath, "ExploreMaterialData.txt"), o => o.materialID, ExploreMaterialDataMap),
			#endregion
			#region F
			LoadExcel(Path.Combine(excelPath, "FeatureTagData.txt"), o => o.tagID, FeatureTagDataMap),
            LoadExcel(Path.Combine(excelPath, "FeatureTagGroupData.txt"), o => o.groupID, FeatureTagGroupDataMap),
            LoadExcel(Path.Combine(excelPath, "FetterCharacterCardData.txt"), o => o.avatarId, FetterCharacterCardDataMap),
            FettersData.Load(Path.Combine(excelPath, "FettersData.txt"), o => o.fetter_id, FettersDataMap),
            FetterInfoData.Load(Path.Combine(excelPath, "FetterDataIformation.txt"), o => o.fetter_id, FetterInfoDataMap),
            FetterStoryData.Load(Path.Combine(excelPath, "FetterDataStory.txt"), o => o.fetter_id, FetterStoryDataMap),
            LoadExcel(Path.Combine(excelPath, "FireworksData.txt"), o => o.materialID, FireworksDataMap),
            LoadExcel(Path.Combine(excelPath, "FireworksLaunchData.txt"), o => o.launchParamType, FireworksLaunchDataMap),
            LoadExcel(Path.Combine(excelPath, "FishBaitData.txt"), o => o.id, FishBaitDataMap),
            LoadExcel(Path.Combine(excelPath, "FishData.txt"), o => o.id, FishDataMap),
            LoadExcel(Path.Combine(excelPath, "FishPoolData.txt"), o => o.id, FishPoolDataMap),
            LoadExcel(Path.Combine(excelPath, "FishRodData.txt"), o => o.id, FishRodDataMap),
            LoadExcel(Path.Combine(excelPath, "FishSkillData.txt"), o => o.id, FishSkillDataMap),
            LoadExcel(Path.Combine(excelPath, "FishStockData.txt"), o => o.id, FishStockDataMap),
            LoadExcel(Path.Combine(excelPath, "FleurFairBuffEnergyStatData.txt"), o => o.id, FleurFairBuffEnergyStatDataMap),
            LoadExcel(Path.Combine(excelPath, "FleurFairChapterData.txt"), o => o.id, FleurFairChapterDataMap),
            LoadExcel(Path.Combine(excelPath, "FleurFairDungeonData.txt"), o => o.id, FleurFairDungeonDataMap),
            LoadExcel(Path.Combine(excelPath, "FleurFairDungeonStatData.txt"), o => o.id, FleurFairDungeonStatDataMap),
            LoadExcel(Path.Combine(excelPath, "FleurFairMiniGameData.txt"), o => o.id, FleurFairMiniGameDataMap),
            LoadExcel(Path.Combine(excelPath, "FleurFairPreviewData.txt"), o => o.id, FleurFairPreviewDataMap),
            LoadExcel(Path.Combine(excelPath, "FlightActivityData.txt"), o => o.id, FlightActivityDataMap),
            LoadExcel(Path.Combine(excelPath, "FlightActivityDayData.txt"), o => o.id, FlightActivityDayDataMap),
            LoadExcel(Path.Combine(excelPath, "FlightActivityMedalData.txt"), o => o.id, FlightActivityMedalDataMap),
            LoadExcel(Path.Combine(excelPath, "ForgeData.txt"), o => o.id, ForgeDataMap),
            LoadExcel(Path.Combine(excelPath, "ForgeUpdateData.txt"), o => o.playerLevel, ForgeUpdateDataMap),
            LoadExcel(Path.Combine(excelPath, "FungusFighterBase.txt"), o => o.activityId, FungusFighterBaseMap),
            LoadExcel(Path.Combine(excelPath, "FungusFighterCatchLevel.txt"), o => o.campId, FungusFighterCatchLevelMap),
            LoadExcel(Path.Combine(excelPath, "FungusFighterCultivate.txt"), o => o.levelId, FungusFighterCultivateMap),
            LoadExcel(Path.Combine(excelPath, "FungusFighterDungeon.txt"), o => o.DungeonID, FungusFighterDungeonMap),
            LoadExcel(Path.Combine(excelPath, "FungusFighterDungeonQST.txt"), o => o.plotID, FungusFighterDungeonQSTMap),
            LoadExcel(Path.Combine(excelPath, "FungusFighterMonster.txt"), o => o.id, FungusFighterMonsterMap),
            LoadExcel(Path.Combine(excelPath, "FungusFighterName.txt"), o => o.id, FungusFighterNameMap),
            LoadExcel(Path.Combine(excelPath, "FurnitureMakeData.txt"), o => o.configID, FurnitureMakeDataMap),
            LoadExcel(Path.Combine(excelPath, "FurnitureSuiteExcelData.txt"), o => o.suiteID, FurnitureSuiteDataMap),
            LoadExcel(Path.Combine(excelPath, "FurnitureTypeExcelData.txt"), o => o.typeID, FurnitureTypeDataMap),
			#endregion
			#region G
			LoadExcel(Path.Combine(excelPath, "GachaNewbieData.txt"), o => o.SinglePullItemID, GachaNewbieDataMap),
            LoadExcel(Path.Combine(excelPath, "GachaPoolData.txt"), o => Tuple.Create(o.PoolID, o.ItemID), GachaPoolDataMap),
            LoadExcel(Path.Combine(excelPath, "GachaPoolData.txt"), o => Tuple.Create(o.ProbabilityRuleID, o.PropParentType), GachaProbDataMap),
            LoadExcel(Path.Combine(excelPath, "GachaRestrictData.txt"), o => o.GachaPoolID, GachaRestrictDataMap),
            LoadExcel(Path.Combine(excelPath, "GachaRuleData.txt"), o => o.RuleID, GachaRuleDataMap),
            LoadExcel(Path.Combine(excelPath, "GachaTokenDropData.txt"), o => o.ItemSubType, GachaTokenDropDataMap),
            LoadExcel(Path.Combine(excelPath, "GachaWish.txt"), o => o.GachaPoolID, GachaWishMap),
            LoadExcel(Path.Combine(excelPath, "GadgetChainData.txt"), o => o.chainId, GadgetChainDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetCurveData.txt"), o => o.level, GadgetCurveDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_AbilitySpecial.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_Affix.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_Avatar.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_Equip.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_FishingRod.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_Homeworld.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_Level.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_Monster.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_Quest.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetData_Vehicle.txt"), o => o.id, GadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetGivingData.txt"), o => o.Id, GivingDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetInteractData.txt"), o => o.interactId, GadgetInteractDataMap),
            LoadExcel(Path.Combine(excelPath, "GadgetLandSound.txt"), o => o.id, GadgetLandSoundMap),
            LoadExcel(Path.Combine(excelPath, "GadgetPropData.txt"), o => o.id, GadgetPropDataMap),
            LoadExcel(Path.Combine(excelPath, "GalleryData.txt"), o => o.id, GalleryDataMap),
            LoadExcel(Path.Combine(excelPath, "GatherBundleData.txt"), o => o.id, GatherBundleDataMap),
            LoadExcel(Path.Combine(excelPath, "GatherData.txt"), o => Tuple.Create(o.id, o.gadgetId), GatherDataMap),
            LoadExcel(Path.Combine(excelPath, "GivingData.txt"), o => o.Id, GivingDataMap),
            LoadExcel(Path.Combine(excelPath, "GivingData_ActivitySpice.txt"), o => o.Id, GivingDataMap),
            LoadExcel(Path.Combine(excelPath, "GivingData_Item_list.txt"), o => o.Id, GivingItemListMap),
            LoadExcel(Path.Combine(excelPath, "GlobalWatcherData.txt"), o => o.id, GlobalWatcherDataMap),
            LoadExcel(Path.Combine(excelPath, "GravenCampData.txt"), o => o.id, GravenCampDataMap),
            LoadExcel(Path.Combine(excelPath, "GravenCampPhotoData.txt"), o => o.ID, GravenCampPhotoDataMap),
            LoadExcel(Path.Combine(excelPath, "GravenCampRaceData.txt"), o => o.ID, GravenCampRaceDataMap),
            LoadExcel(Path.Combine(excelPath, "GravenCampStageData.txt"), o => o.StageID, GravenCampStageDataMap),
            LoadExcel(Path.Combine(excelPath, "GravenCarveOverallData.txt"), o => o.ID, GravenCarveOverallDataMap),
            LoadExcel(Path.Combine(excelPath, "GravenCarveStageData.txt"), o => o.ID, GravenCarveStageDataMap),
            LoadExcel(Path.Combine(excelPath, "GravenInnocenceData.txt"), o => o.activityID, GravenInnocenceDataMap),
            LoadExcel(Path.Combine(excelPath, "GravenPhotoObjectData.txt"), o => o.ID, GravenPhotoObjectDataMap),
            LoadExcel(Path.Combine(excelPath, "GroupLinksBundleData.txt"), o => o.Id, GroupLinksBundleDataMap),
            LoadExcel(Path.Combine(excelPath, "GroupLinksBundleRewardData.txt"), o => o.rewardId, GroupLinksBundleRewardDataMap),
            LoadExcel(Path.Combine(excelPath, "GroupTagData.txt"), o => o.id, GroupTagDataMap),
			#endregion
			#region H
			LoadExcel(Path.Combine(excelPath, "H5ActivityData.txt"), o => o.h5ActivityId, H5ActivityDataMap),
            H5ActivityWatcherExcelConfig.Load(Path.Combine(excelPath, "H5ActivityWatcherData.txt"), o => o.id, H5ActivityWatcherDataMap),
            LoadExcel(Path.Combine(excelPath, "HideAndSeekSkillData.txt"), o => o.id, HideAndSeekSkillDataMap),
            LoadExcel(Path.Combine(excelPath, "HitLevelTemplateData.txt"), o => o.type, HitLevelTemplateDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeworldAnimalExcelData.txt"), o => o.furnitureID, HomeworldAnimalDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldAreaComfortData.txt"), o => o.configID, HomeWorldAreaComfortDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldBgmData.txt"), o => o.bgmID, HomeWorldBgmDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeworldBlueprintSlotData.txt"), o => o.ID, HomeworldBlueprintSlotDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldComfortLevelData.txt"), o => o.levelID, HomeWorldComfortLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldEventData.txt"), o => o.eventID, HomeWorldEventDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldExtraFurnitureData.txt"), o => o.furnitureID, HomeWorldExtraFurnitureDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldFieldData.txt"), o => o.fieldGadgetID, HomeWorldFieldDataMap),
            LoadExcel<ItemConfig, uint, HomeWorldFurnitureData>(Path.Combine(excelPath, "FurnitureExcelData.txt"), o => o.id, ItemDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldLeastShopData.txt"), o => Tuple.Create(o.level, o.poolID), HomeWorldLeastShopDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldLevelExeclData.txt"), o => o.level, HomeworldLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldLimitShopData.txt"), o => o.goodsId, HomeWorldLimitShopDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldModuleData.txt"), o => o.Id, HomeworldModuleDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldNPCConfigData.txt"), o => o.furnitureID, HomeWorldNPCDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldPlantData.txt"), o => o.seedID, HomeWorldPlantDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldServerGadgetExcel.txt"), o => o.furnitureId, HomeWorldServerGadgetDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldShopSubData.txt"), o => o.subID, HomeWorldShopSubTagDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldWoodData.txt"), o => o.WoodID, HomeWorldWoodDataMap),
            LoadExcel(Path.Combine(excelPath, "HuntingClueGatherData.txt"), o => o.configId, HuntingClueGatherDataMap),
            LoadExcel(Path.Combine(excelPath, "HuntingClueMonsterData.txt"), o => o.configId, HuntingClueMonsterDataMap),
            LoadExcel(Path.Combine(excelPath, "HuntingGroupInfoData.txt"), o => o.groupId, HuntingGroupInfoDataMap),
            LoadExcel(Path.Combine(excelPath, "HuntingMonsterData.txt"), o => o.configId, HuntingMonsterDataMap),
            LoadExcel(Path.Combine(excelPath, "HuntingRefreshData.txt"), o => o.id, HuntingRefreshDataMap),
            LoadExcel(Path.Combine(excelPath, "HuntingRegionData.txt"), o => o.id, HuntingRegionDataMap),
			#endregion
			#region I
			LoadExcel(Path.Combine(excelPath, "InferenceConclusionData.txt"), o => o.GroupConclusionID, InferenceConclusionDataMap),
			LoadExcel(Path.Combine(excelPath, "InferencePageData.txt"), o => o.PageID, InferencePageDataMap),
			LoadExcel(Path.Combine(excelPath, "InferenceWordData.txt"), o => o.EntryID, InferenceWordDataMap),
			LoadExcel(Path.Combine(excelPath, "InstableSprayBuffData.txt"), o => o.id, InstableSprayBuffDataMap),
			LoadExcel(Path.Combine(excelPath, "InstableSprayDifficultyData.txt"), o => o.DifficultyLevel, InstableSprayDifficultyDataMap),
			LoadExcel(Path.Combine(excelPath, "InstableSprayGachaData.txt"), o => o.IntraPhase, InstableSprayGachaDataMap),
			LoadExcel(Path.Combine(excelPath, "InstableSprayLevelData.txt"), o => o.LevelID, InstableSprayLevelDataMap),
			LoadExcel(Path.Combine(excelPath, "InstableSprayOverallData.txt"), o => o.ActivityID, InstableSprayOverallDataMap),
			LoadExcel(Path.Combine(excelPath, "InstableSprayStageData.txt"), o => o.ActivityStage, InstableSprayStageDataMap),
			LoadExcel(Path.Combine(excelPath, "InvestigationData.txt"), o => o.id, InvestigationDataMap),
			LoadExcel(Path.Combine(excelPath, "InvestigationDungeonData.txt"), o => o.entranceId, InvestigationDungeonDataMap),
			LoadExcel(Path.Combine(excelPath, "InvestigationMonsterData.txt"), o => o.id, InvestigationMonsterDataMap),
			InvestigationTargetConfig.Load(Path.Combine(excelPath, "InvestigationCharAscendData.txt"), o => o.id, InvestigationTargetDataMap),
			InvestigationTargetConfig.Load(Path.Combine(excelPath, "InvestigationTargetData.txt"), o => o.id, InvestigationTargetDataMap),
			InvestigationTargetConfig.Load(Path.Combine(excelPath, "InvestigationHomeworldData.txt"), o => o.id, InvestigationTargetDataMap),
			LoadExcel(Path.Combine(excelPath, "IrodoriChessCardData.txt"), o => o.ID, IrodoriChessCardDataMap),
			LoadExcel(Path.Combine(excelPath, "IrodoriChessDisorderData.txt"), o => o.disorderId, IrodoriChessDisorderDataMap),
			LoadExcel(Path.Combine(excelPath, "IrodoriChessGearData.txt"), o => o.gearId, IrodoriChessGearDataMap),
			LoadExcel(Path.Combine(excelPath, "IrodoriChessLevelData.txt"), o => o.levelId, IrodoriChessLevelDataMap),
			LoadExcel(Path.Combine(excelPath, "IrodoriChessMapData.txt"), o => o.mapId, IrodoriChessMapDataMap),
			LoadExcel(Path.Combine(excelPath, "IrodoriChessMapPointData.txt"), o => o.ID, IrodoriChessMapPointDataMap),
			#endregion
			#region L
			LoadExcel(Path.Combine(excelPath, "LanV2FireworkChallengeData.txt"), o => o.challengeId, LanV2FireworksChallengeDataMap),
            LoadExcel(Path.Combine(excelPath, "LanV2FireworkFactorData.txt"), o => o.factorId, LanV2FireworksFactorDataMap),
            LoadExcel(Path.Combine(excelPath, "LanV2FireworkOverallData.txt"), o => o.scheduleId, LanV2FireworksOverallDataMap),
            LoadExcel(Path.Combine(excelPath, "LanV2FireworkSkillData.txt"), o => o.skillId, LanV2FireworksSkillDataMap),
            LoadExcel(Path.Combine(excelPath, "LanV2FireworkStageData.txt"), o => o.stageId, LanV2FireworksStageDataMap),
            LoadExcel(Path.Combine(excelPath, "LanV2OverAllData.txt"), o => o.id, LanV2OverAllDataMap),
            LoadExcel(Path.Combine(excelPath, "LanV2ProjectionLevelData.txt"), o => o.levelId, LanV2ProjectionLevelMap),
            LoadExcel(Path.Combine(excelPath, "LevelSuppressData.txt"), o => o.level, LevelSuppressMap),
            LoadExcel(Path.Combine(excelPath, "LevelTagData.txt"), o => o.ID, LevelTagMap),
            LoadExcel(Path.Combine(excelPath, "LevelTagGroupsData.txt"), o => o.ID, LevelTagGroupsMap),
            LoadExcel(Path.Combine(excelPath, "LevelTagMapAreaData.txt"), o => o.LevelTagID, LevelTagMapAreaMap),
            LoadExcel(Path.Combine(excelPath, "LevelTagResetData.txt"), o => o.ID, LevelTagResetMap),
            LoadExcel(Path.Combine(excelPath, "LoadingSituationData.txt"), o => o.stageID, LoadingSituationMap),
            LoadExcel(Path.Combine(excelPath, "LoadingTipsData.txt"), o => o.id, LoadingTipsMap),
            LoadExcel(Path.Combine(excelPath, "LockTemplateData.txt"), o => o.type, LockTemplateMap),
            LoadExcel(Path.Combine(excelPath, "LunaRiteBattle.txt"), o => o.Id, LunaRiteBattleMap),
            LoadExcel(Path.Combine(excelPath, "LunaRiteBattleBuff.txt"), o => o.Id, LunaRiteBattleBuffMap),
            LoadExcel(Path.Combine(excelPath, "LunaRitePreviewData.txt"), o => o.Id, LunaRitePreviewDataMap),
            LoadExcel(Path.Combine(excelPath, "LunaRiteSearching.txt"), o => o.Id, LunaRiteSearchingMap),
			#endregion
			#region M
			MaterialData.Load(Path.Combine(excelPath, "MaterialData.txt"), o => o.id, ItemDataMap),
            LoadExcel(Path.Combine(excelPath, "MaterialCodexData.txt"), o => o.Id, MaterialCodexDataMap),
            LoadExcel(Path.Combine(excelPath, "MonsterCurveData.txt"), o => o.level, MonsterCurveDataMap),
            LoadExcel(Path.Combine(excelPath, "MonsterData.txt"), o => o.id, MonsterDataMap),
            LoadExcel(Path.Combine(excelPath, "MonsterDescribeData.txt"), o => o.id, MonsterDescribeDataMap),
            LoadExcel(Path.Combine(excelPath, "MusicGameBasicData.txt"), o => o.id, MusicGameBasicDataMap),
			#endregion
			#region N
			LoadExcel(Path.Combine(excelPath, "NewActivityData.txt"), o => o.activityId, NewActivityDataMap),
            LoadExcel(Path.Combine(excelPath, "NewActivityWatcherData.txt"), o => o.id, NewActivityWatcherDataMap),
            LoadExcel(Path.Combine(excelPath, "NpcData.txt"), o => o.id, NpcDataMap),
            LoadExcel(Path.Combine(excelPath, "NpcFirstMet.txt"), o => o.id, NpcFirstMetMap),
			#endregion
			#region O
			LoadExcel(Path.Combine(excelPath, "OpenStateData.txt"), o => o.id, OpenStateDataMap),
			#endregion
			#region P
			LoadExcel(Path.Combine(excelPath, "PersonalLineData.txt"), o => o.id, PersonalLineDataMap),
            PhotographExpressionData.Load(Path.Combine(excelPath, "PhotographExpressionName.txt"), o => o.fetter_id, PhotographExpressionDataMap),
            PhotographPosenameData.Load(Path.Combine(excelPath, "PhotographPoseName.txt"), o => o.fetter_id, PhotographPosenameDataMap),
            LoadExcel(Path.Combine(excelPath, "PlayerLevelData.txt"), o => o.level, PlayerLevelDataMap),
            ProudSkillData.Load(Path.Combine(excelPath, "ProudSkillData.txt"), o => o.proudSkillId, ProudSkillDataMap),
			#endregion
			#region Q
			LoadExcel(Path.Combine(excelPath, "QuestCodexData.txt"), o => o.Id, QuestCodexDataMap),
            QuestData.Load(Path.Combine(excelPath, "QuestData.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext(){ GuideParamCount = 5, FinishExecCount = 4, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[]{ 2,2,1 },
                    FailCondParamCount = 1, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_Activity.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext(){ GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[]{ 2, 2, 1 },
                    FailCondParamCount = 1, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_Coop.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 5, FinishExecCount = 4, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[] { 2, 2, 1 },
                    FailCondParamCount = 1, FailCondCount = 2 , FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_Exported.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 5, FinishExecCount = 5, AcceptCondParamCount = 5, AcceptCondCount = 10, FinishCondParamCount = new uint[]{ 2, 2, 2, 2, 2, 2 },
                    FailCondParamCount = 2, FailCondCount = 6, FinishExecParamCount = 5, FailExecCount = 5, FailExecParamCount = 5, BeginExecCount = 5, BeginExecParamCount = 5 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_LiyueIQ.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[] { 2, 1, 1 },
                    FailCondParamCount = 2, FailCondCount = 2, FinishExecParamCount = 3, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_LiyueIQ_2.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 4, FinishCondParamCount = new uint[] { 2, 1, 1, 1 },
                    FailCondParamCount = 2, FailCondCount = 2, FinishExecParamCount = 3, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_LiyueIQ_3.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 4, FinishCondParamCount = new uint[] { 2, 1, 1, 1 },
                    FailCondParamCount = 2, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_LiyueLQ_Adult.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[]{ 2, 2, 1 },
                    FailCondParamCount = 1, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 2, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_LiyueMQ.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 4, FinishCondParamCount = new uint[] { 2, 1, 1, 1 },
                    FailCondParamCount = 2, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_LiyueWQ.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[]{ 2, 1, 1 },
                    FailCondParamCount = 1, FailCondCount = 2, FinishExecParamCount = 3, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 2, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_MengdeIQ.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 4, AcceptCondParamCount = 3, AcceptCondCount = 4, FinishCondParamCount = new uint[] { 2, 1, 1, 1 },
                    FailCondParamCount = 2, FailCondCount = 2, FinishExecParamCount = 3, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_MengdeIQ_2.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 4, FinishCondParamCount = new uint[] { 2, 1, 1, 1 },
                    FailCondParamCount = 2, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_MengdeLQ_Adult.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 4, FinishCondParamCount = new uint[] { 2, 1, 1, 2 },
                    FailCondParamCount = 1, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 2, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_MengdeLQ_Teen.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[] { 2, 1, 1 },
                    FailCondParamCount = 1, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }),
            QuestData.Load(Path.Combine(excelPath, "QuestData_MengdeMQ.txt"), o => o.subId, QuestDataMap,
                new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[] { 2, 1, 1 },
                    FailCondParamCount = 1, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }), //actually empty...
			QuestData.Load(Path.Combine(excelPath, "QuestData_NPC.txt"), o => o.subId, QuestDataMap,
				new QuestDataLoadContext() { GuideParamCount = 2, FinishExecCount = 3, AcceptCondParamCount = 3, AcceptCondCount = 3, FinishCondParamCount = new uint[] { 2, 1, 1 },
					FailCondParamCount = 1, FailCondCount = 2, FinishExecParamCount = 2, FailExecCount = 3, FailExecParamCount = 2, BeginExecCount = 3, BeginExecParamCount = 2 }), //actually empty...
			LoadExcel(Path.Combine(excelPath, "QuestGlobalVar.txt"), o => o.id, QuestGlobalVarConfigMap),
            LoadExcel(Path.Combine(excelPath, "QuestResCollection.txt"), o => o.id, QuestResCollectionExcelConfigMap),
            LoadExcel(Path.Combine(excelPath, "QuestSpecialShow.txt"), o => o.id, QuestSpecialShowConfigMap),
			#endregion
			#region R
			LoadExcel(Path.Combine(excelPath, "RandTaskData.txt"), o => o.ID, RandTaskDataMap),
            LoadExcel(Path.Combine(excelPath, "RandTaskLevelData.txt"), o => o.ID, RandTaskLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "RandTaskRewardData.txt"), o => o.ID, RandTaskRewardDataMap),
            LoadExcel(Path.Combine(excelPath, "ReactionEnergyData.txt"), o => o.type, ReactionEnergyDataMap),
            LoadExcel(Path.Combine(excelPath, "RefreshPolicyData.txt"), o => o.id, RefreshPolicyDataMap),
            LoadExcel(Path.Combine(excelPath, "ReliquaryAffixData.txt"), o => o.id, ReliquaryAffixDataMap),
			LoadExcel(Path.Combine(excelPath, "ReliquaryCodexData.txt"), o => o.Id, ReliquaryCodexDataMap),
			LoadExcel<ItemConfig, uint, ReliquaryData>(Path.Combine(excelPath, "ReliquaryData.txt"), o => o.id, ItemDataMap),
            LoadExcel(Path.Combine(excelPath, "ReliquaryDecomposeData.txt"), o => o.id, ReliquaryDecomposeDataMap),
            LoadExcel(Path.Combine(excelPath, "ReliquaryLevelData.txt"), o => Tuple.Create(o.rank, o.level), ReliquaryLevelDataMap),
			LoadExcel(Path.Combine(excelPath, "ReliquaryMainData.txt"), o => o.id, ReliquaryMainPropDataMap),
			LoadExcel(Path.Combine(excelPath, "ReliquaryPowerupData.txt"), o => o.powerupMultiple, ReliquaryPowerupDataMap),
			LoadExcel(Path.Combine(excelPath, "ReliquarySetData.txt"), o => o.setId, ReliquarySetDataMap),
			LoadExcel(Path.Combine(excelPath, "ReminderData_Exported.txt"), o => o.id, ReminderDataMap),
			LoadExcel(Path.Combine(excelPath, "ReminderData.txt"), o => o.id, ReminderIndexDataMap),
            LoadExcel(Path.Combine(excelPath, "ReputationCityData.txt"), o => o.cityId, ReputationCityDataMap),
            LoadExcel(Path.Combine(excelPath, "ReputationEntrance.txt"), o => Tuple.Create(o.textId, o.cityId), ReputationEntranceMap),
            LoadExcel(Path.Combine(excelPath, "ReputationExplore.txt"), o => o.exploreId, ReputationExploreMap),
            LoadExcel(Path.Combine(excelPath, "ReputationLevel.txt"), o => o.id, ReputationLevelMap),
            LoadExcel(Path.Combine(excelPath, "ReputationQuest.txt"), o => o.ParentQuestId, ReputationQuestMap),
            LoadExcel(Path.Combine(excelPath, "ReputationRequest.txt"), o => o.RequestId, ReputationRequestMap),
            LoadExcel(Path.Combine(excelPath, "ReunionMissionData.txt"), o => o.id, ReunionMissionDataMap),
            LoadExcel(Path.Combine(excelPath, "ReunionPrivilegeData.txt"), o => o.id, ReunionPrivilegeDataMap),
            LoadExcel(Path.Combine(excelPath, "ReunionScheduleData.txt"), o => o.id, ReunionScheduleDataMap),
            LoadExcel(Path.Combine(excelPath, "ReunionSignInData.txt"), o => Tuple.Create(o.id, o.dayIndex), ReunionSignInDataMap),
            //LoadExcel(Path.Combine(excelPath, "ReunionWatcherData.txt"), o => o.watcherGroupId, ReunionWatcherDataMap),
            LoadExcel(Path.Combine(excelPath, "ReviseLevelGrowData.txt"), o => o.id, ReviseLevelGrowDataMap),
            LoadExcel(Path.Combine(excelPath, "RewardData.txt"), o => o.rewardId, RewardDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueCellWeightData.txt"), o => o.id, RogueCellWeightDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueDiaryBuffData.txt"), o => o.id, RogueDiaryBuffDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueDiaryCardWeightData.txt"), o => o.id, RogueDiaryCardWeightDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueDiaryDungeonData.txt"), o => o.dungeonId, RogueDiaryDungeonDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueDiaryPreviewData.txt"), o => o.activityId, RogueDiaryPreviewDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueDiaryResourceData.txt"), o => o.id, RogueDiaryResourceDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueDungeonCellData.txt"), o => o.id, RogueDungeonCellDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueGadgetRot.txt"), o => o.id, RogueGadgetRotMap),
            LoadExcel(Path.Combine(excelPath, "RogueMonsterPoolData.txt"), o => o.id, RogueMonsterPoolDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueSequenceData.txt"), o => o.id, RogueSequenceDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueStageData.txt"), o => o.stageId, RogueStageDataMap),
            LoadExcel(Path.Combine(excelPath, "RogueTokenData.txt"), o => o.id, RogueTokenDataMap),
            LoadExcel(Path.Combine(excelPath, "RoomData.txt"), o => o.id, RoomDataMap),
            #endregion
			#region S
			LoadExcel(Path.Combine(excelPath, "SceneData.txt"), o => o.id, SceneDataMap),
            LoadExcel(Path.Combine(excelPath, "SceneTagData.txt"), o => o.id, SceneTagDataMap),
            LoadExcel(Path.Combine(excelPath, "ShopData.txt"), o => o.shopId, ShopDataMap),
            LoadExcel(Path.Combine(excelPath, "HomeWorldShopData.txt"), o => o.goodsId, ShopGoodsDataMap),
            LoadExcel(Path.Combine(excelPath, "ShopGoodsData.txt"), o=> o.goodsId, ShopGoodsDataMap),
			#endregion
			#region T
			LoadExcel(Path.Combine(excelPath, "TeamResonanceData.txt"), o => o.teamResonanceId, TeamResonanceDataMap),
            LoadExcel(Path.Combine(excelPath, "TowerFloorData.txt"), o => o.floorId, TowerFloorDataMap),
            LoadExcel(Path.Combine(excelPath, "TowerLevelData.txt"), o => o.levelId, TowerLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "TowerScheduleData.txt"), o => o.scheduleId, TowerScheduleDataMap),
            LoadExcel(Path.Combine(excelPath, "TriggerData.txt"), o => o.id, TriggerDataMap),
			#endregion
			#region W
			LoadExcel(Path.Combine(excelPath, "WeaponCodexData.txt"), o => o.Id, WeaponCodexDataMap),
            LoadExcel(Path.Combine(excelPath, "WeaponCurveData.txt"), o => o.level, WeaponCurveDataMap),
            LoadExcel<ItemConfig, uint, WeaponData>(Path.Combine(excelPath, "WeaponData.txt"), o => o.id, ItemDataMap),
            LoadExcel(Path.Combine(excelPath, "WeaponLevelData.txt"), o => o.level, WeaponLevelDataMap),
            LoadExcel(Path.Combine(excelPath, "WeaponPromoteData.txt"), o => Tuple.Create(o.weaponPromoteId, o.promoteLevel), WeaponPromoteDataMap),
            LoadExcel(Path.Combine(excelPath, "WeatherData.txt"), o => o.areaID, WeatherDataMap),
            LoadExcel(Path.Combine(excelPath, "WorldAreaData.txt"), o => o.ID, WorldAreaDataMap),
            LoadExcel(Path.Combine(excelPath, "WorldLevelData.txt"), o => o.level, WorldLevelDataMap),
			#endregion

			LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "ActivityAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "AnimalAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "AvatarAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "ChallengeAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "EquipAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "GadgetAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "LevelAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "MonsterAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "MonsterAffixAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "QATestAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "QuestAbilities"), ConfigAbilityMap),
            LoadConfigAbility(Path.Combine(binPath, "Ability", "Temp", "TeamAbilities"), ConfigAbilityMap),
			//LoadBinOutFolder(Path.Combine(binPath, "scene_npc_born"), o => o.sceneId,  SceneNpcBornDataMap),
			LoadBinOutFolder(Path.Combine(binPath, "Ability_group"), AbilityGroupDataMap),
            LoadBinOutFolder(Path.Combine(binPath, "Talent", "AvatarTalents"), AvatarTalentConfigDataMap, false),
            LoadBinOutFolder(Path.Combine(binPath, "Avatar"), ConfigAvatarMap, false),
            LoadBinOutFolder(Path.Combine(binPath, "Gadget"), ConfigGadgetMap, true),
			//LoadBinOutFolder(Path.Combine(binPath, "Quest"), q => q.id, MainQuestDataMap),
			LoadBinOutFolder(Path.Combine(binPath, "Talent", "EquipTalents"), WeaponAffixConfigDataMap),
            LoadBinOutFolder(Path.Combine(binPath, "Talent", "RelicTalents"), RelicAffixConfigDataMap),
            LoadBinOutFolder(Path.Combine(binPath, "Talent", "TeamTalents"), TeamResonanceConfigDataMap),
			//LoadBinOutFolder(Path.Combine(binPath, "Scene", "Point"), ScenePointDataMap, false),
        });
        watch.Stop();
        Logger.WriteLine($"Finished Loading Resources. Time Elapsed: {watch.ElapsedMilliseconds}ms");
    }
}