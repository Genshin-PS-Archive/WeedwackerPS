using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Data.BinOut.AbilityGroup;
using Weedwacker.GameServer.Data.BinOut.Avatar;
using Weedwacker.GameServer.Data.BinOut.Shared.ConfigEntity;
using Weedwacker.GameServer.Data.BinOut.Talent;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Data.Excel.Common;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Avatar
{
	// ONLY ONE INSTANCE PER AVATAR ID
	internal class AvatarCompiledData
	{
		public readonly uint AvatarId;
		public readonly AvatarData GeneralData;
		public readonly ConfigAvatar ConfigAvatar;
		public readonly Dictionary<uint, AvatarSkillDepotData> SkillDepotData = new(); // <depotId,depot> Skill, SubSkill, Talent, and ProudSkill ids. So far only the twins have multiple. Assume first is default
		public readonly Dictionary<uint, Dictionary<uint, AvatarSkillData>> SkillData = new(); // <depotId,<skillId,data>>
		public readonly Dictionary<uint, AvatarPromoteData> PromoteData; // <promoteLevel,data> AKA Ascension
		public readonly Dictionary<uint, Dictionary<uint, AvatarTalentData>> TalentData = new(); // <depotId,<talentId,data>> Constellations and skill upgrades
		public readonly Dictionary<uint, Dictionary<uint, ProudSkillData>> ProudSkillData = new(); // <depotId,<proudSkillId,data>> Passives
		public readonly Dictionary<uint, Dictionary<uint, ConfigAbility>?> AbilityHashMap = new(); //<depotId,<Hashes, config>>
		//public readonly Dictionary<uint, Dictionary<string, ConfigAbility>> AbilityConfigMap = new(); // depotId
		public readonly Dictionary<uint, AvatarCostumeData> CostumeData; // costumeId
		public readonly Dictionary<uint, AvatarCodexData> CodexData; // sortId Codex entry
		public static IDictionary<uint, AvatarLevelData> LevelData => GameData.AvatarLevelDataMap; // <level,exp> Level exp breakpoints
		public static IDictionary<uint, AvatarCurveData> CurveData => GameData.AvatarCurveDataMap; // <level,curveInfo> Base Stat multipliers
		public static IDictionary<uint, AvatarFlycloakData> FlycloakData => GameData.AvatarFlycloakDataMap; // flycloakId
		public readonly Dictionary<uint, Dictionary<string, ConfigTalentMixin[]>> ConfigTalentMap = new(); // <depotId, talentName>
		//Fetters
		public readonly FetterCharacterCardData? CardData;
		public readonly FetterInfoData FetterInfoData; // General info
		public readonly Dictionary<uint, FetterStoryData> FetterStoryData; // fetterId
		public readonly Dictionary<uint, FettersData> FettersData; // fetterId
		public readonly Dictionary<uint, PhotographPosenameData> PhotographPosenameData; // fetterId
		public readonly Dictionary<uint, PhotographExpressionData> PhotographExpressionData; // fetterId
		public static IDictionary<uint, AvatarFetterLevelData> FetterLevelData => GameData.AvatarFetterLevelDataMap; // level Friendship exp breakpoints

		public readonly Tuple<ArithType, float>[] HpGrowthCurve;
		public readonly Tuple<ArithType, float>[] AttackGrowthCurve;
		public readonly Tuple<ArithType, float>[] DefenseGrowthCurve;

		public float BaseCritical => GeneralData.critical;
		public float BaseCriticalHurt => GeneralData.criticalHurt;

		public readonly List<uint> Fetters;
		public readonly uint NameCardRewardId;
		public readonly uint NameCardId;
		public AvatarCompiledData(uint avatarId)
		{
			AvatarId = avatarId;
			GeneralData = GameData.AvatarDataMap[AvatarId];
			CardData = GameData.FetterCharacterCardDataMap.GetValueOrDefault(AvatarId);
			ConfigAvatar = GameData.ConfigAvatarMap[GeneralData.combatConfig];
			CodexData = GameData.AvatarCodexDataMap.Where(w => w.Value.avatarId == AvatarId).ToDictionary(x => x.Key, x => x.Value);
			CostumeData = GameData.AvatarCostumeDataMap.Where(w => w.Value.characterId == AvatarId).ToDictionary(x => x.Key, x => x.Value);
			FettersData = GameData.FettersDataMap.Where(w => w.Value.avatarId == AvatarId).ToDictionary(w => w.Key, w => w.Value);
			FetterInfoData = GameData.FetterInfoDataMap.Where(w => w.Value.avatarId == AvatarId).FirstOrDefault().Value;
			FetterStoryData = GameData.FetterStoryDataMap.Where(w => w.Value.avatarId == AvatarId).ToDictionary(w => w.Key, w => w.Value);
			PhotographExpressionData = GameData.PhotographExpressionDataMap.Where(w => w.Value.avatarId == AvatarId).ToDictionary(w => w.Key, w => w.Value);
			PhotographPosenameData = GameData.PhotographPosenameDataMap.Where(w => w.Value.avatarId == AvatarId).ToDictionary(w => w.Key, w => w.Value);
			PromoteData = GameData.AvatarPromoteDataMap.Where(w => w.Key.Item1 == GeneralData.avatarPromoteId).ToDictionary(x => x.Key.Item2, x => x.Value);
			if (GeneralData.candSkillDepotIds != null && GeneralData.candSkillDepotIds.Length > 0)
			{
				foreach (uint depotId in GeneralData.candSkillDepotIds)
				{
					SkillDepotData.Add(depotId, GameData.AvatarSkillDepotDataMap[depotId]);
				}
			}
			else
			{
				SkillDepotData.Add(GeneralData.skillDepotId, GameData.AvatarSkillDepotDataMap[GeneralData.skillDepotId]);
			}
			foreach (AvatarSkillDepotData depot in SkillDepotData.Values)
			{
				Dictionary<uint, AvatarSkillData> dictionary1 = GameData.AvatarSkillDataMap.Where(w => depot.skills.Contains(w.Key)).ToDictionary(x => x.Key, x => x.Value);
				if (depot.energySkill != null) dictionary1.Add((uint)depot.energySkill, GameData.AvatarSkillDataMap[(uint)depot.energySkill]);
				if(depot.subSkills != null)
				{
					foreach(uint sub in depot.subSkills)
					{
						dictionary1.Add(sub, GameData.AvatarSkillDataMap[sub]);
					}
				}
				SkillData.Add(depot.id, dictionary1);
				Dictionary<uint, AvatarTalentData>? dictionary7 = GameData.AvatarTalentDataMap.Where(w => depot.talents.Contains(w.Value.talentId)).ToDictionary(x => x.Key, x => x.Value);
				TalentData.Add(depot.id, dictionary7);
				Dictionary<uint, ProudSkillData>? dictionary8 = GameData.ProudSkillDataMap.Where(w => depot.inherentProudSkillOpens.Where(y => y.proudSkillGroupId == w.Value.proudSkillGroupId).Any()).ToDictionary(x => x.Key, x => x.Value);
				ProudSkillData.Add(depot.id, dictionary8);
				foreach (AvatarSkillData? skilldata in dictionary1.Values)
				{
					IEnumerable<KeyValuePair<uint, ProudSkillData>>? proudData = GameData.ProudSkillDataMap.Where(w => w.Value.proudSkillGroupId == skilldata.proudSkillGroupId);
					foreach (KeyValuePair<uint, ProudSkillData> proud in proudData)
					{
						ProudSkillData[depot.id][proud.Key] = proud.Value;
					}
				}
				if (!string.IsNullOrEmpty(depot.talentStarName) && GameData.AvatarTalentConfigDataMap.TryGetValue($"Config{depot.talentStarName}", out Dictionary<string, ConfigTalentMixin[]>? configTalents))
				{
					ConfigTalentMap[depot.id] = new Dictionary<string, ConfigTalentMixin[]>();
					foreach(KeyValuePair<string, ConfigTalentMixin[]> kvp in configTalents)
					{
						ConfigTalentMap[depot.id].Add(kvp.Key, kvp.Value);
					}
				}
				Dictionary<uint, ConfigAbility> abilityHashMap = new();
				// add abilityGroup abilities (if avatar skill depot ability group)
				if (!string.IsNullOrEmpty(depot.skillDepotAbilityGroup))
				{
					ConfigAbilityGroup? abilityData = GameData.AbilityGroupDataMap[depot.skillDepotAbilityGroup];
					foreach (ConfigEntityAbilityEntry ability in abilityData.targetAbilities)
					{
						ConfigAbility config = GameData.ConfigAbilityMap[ability.abilityName];
						abilityHashMap[Utils.AbilityHash(ability.abilityName)] = config;
					}
				}
				//add ConfigAvatar abilities
				if (ConfigAvatar.abilities != null)
					foreach (ConfigEntityAbilityEntry ability in ConfigAvatar.abilities)
					{
						ConfigAbility config = GameData.ConfigAbilityMap[ability.abilityName];
						abilityHashMap[Utils.AbilityHash(ability.abilityName)] = config;
					}
				//add GlobalCombat default abilities
				foreach (string abilityName in GameData.GlobalCombatData.defaultAbilities.defaultAvatarAbilities)
				{
					ConfigAbility config = GameData.ConfigAbilityMap[abilityName];
					abilityHashMap[Utils.AbilityHash(config.abilityName)] = config;
				}
				AbilityHashMap.Add(depot.id, abilityHashMap);
			}

			HpGrowthCurve = new Tuple<ArithType, float>[CurveData.Count];
			AttackGrowthCurve = new Tuple<ArithType, float>[CurveData.Count];
			DefenseGrowthCurve = new Tuple<ArithType, float>[CurveData.Count];
			foreach (AvatarCurveData curveData in CurveData.Values)
			{
				uint level = curveData.level - 1;
				foreach (FightPropGrowConfig growCurve in GeneralData.propGrowCurves)
				{
					if (growCurve.type == null || growCurve.grow_curve == null) continue;
					switch (growCurve.type)
					{
						case FightPropType.FIGHT_PROP_BASE_HP:
							HpGrowthCurve[level] = curveData.GetArith((GrowCurveType)growCurve.grow_curve);
							break;
						case FightPropType.FIGHT_PROP_BASE_ATTACK:
							AttackGrowthCurve[level] = curveData.GetArith((GrowCurveType)growCurve.grow_curve);
							break;
						case FightPropType.FIGHT_PROP_BASE_DEFENSE:
							DefenseGrowthCurve[level] = curveData.GetArith((GrowCurveType)growCurve.grow_curve);
							break;
						default:
							Logger.WriteErrorLine("Error loading Avatar Growth Curves");
							break;
					}
				}
			}
		}

		private static float CalcValue(float value, Tuple<ArithType, float> curve)
		{
			switch (curve.Item1)
			{
				case ArithType.ARITH_MULTI:
					return value * curve.Item2;
				case ArithType.ARITH_ADD:
					return value + curve.Item2;
				default:
					Logger.WriteErrorLine($"Unhandled Avatar curve operation {curve.Item1}");
					goto case ArithType.ARITH_MULTI;
			}
		}
		public float GetBaseHp(uint level) => CalcValue(GeneralData.hp_base, HpGrowthCurve[level - 1]);

		public float GetBaseAttack(uint level) => CalcValue(GeneralData.attack_base, AttackGrowthCurve[level - 1]);

		public float GetBaseDefense(uint level) => CalcValue(GeneralData.defense_base, DefenseGrowthCurve[level - 1]);
	}
}
