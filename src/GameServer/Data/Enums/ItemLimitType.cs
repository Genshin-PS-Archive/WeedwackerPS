
namespace Weedwacker.GameServer.Data.Enums;

public enum ItemLimitType : int
{
	ITEM_LIMIT_NONE = 0,
	ITEM_LIMIT_UNLIMITED = 1,
	ITEM_LIMIT_GM = 2,
	ITEM_LIMIT_QUEST = 3,
	ITEM_LIMIT_CITY_UPGRADE = 4,
	ITEM_LIMIT_UNLOCK_TRANS_POINT = 5,
	ITEM_LIMIT_UNLOCK_DUNGEON = 6,
	ITEM_LIMIT_INVESTIGATION = 7,
	ITEM_LIMIT_PLAYER_LEVEL_UPGRADE = 8,
	ITEM_LIMIT_PUSH_TIPS_REWARD = 9,
	ITEM_LIMIT_AVATAR_FETTER_REWARD = 10,
	ITEM_LIMIT_WORLD_AREA_EXPLORE_EVENT_REWARD = 11,
	ITEM_LIMIT_ACTIVITY_SEA_LAMP = 12,
	ITEM_LIMIT_BATTLE_PASS_PAID_REWARD = 13,
	ITEM_LIMIT_ONEOFF_BIG_WORLD_DROP = 14,
	ITEM_LIMIT_GAMEPLAY_NICHE = 15,
	ITEM_LIMIT_ONEOFF_DUNGEON_DROP = 16,
	ITEM_LIMIT_ONEOFF_PLUNDER_DROP = 17,
	ITEM_LIMIT_ONEOFF_SUBFIELD_DROP = 18,
	ITEM_LIMIT_BIG_WORLD_CHEST = 19,
	ITEM_LIMIT_GACHA_TOKEN_DROP = 20,
	ITEM_LIMIT_DAILY_BIG_WORLD_DROP = 22,
	ITEM_LIMIT_DAILY_DUNGEON_DROP = 23,
	ITEM_LIMIT_DAILY_PLUNDER_DROP = 24,
	ITEM_LIMIT_DAILY_SUBFIELD_DROP = 25,
	ITEM_LIMIT_DAILY_SEA_LAMP_DROP = 26,
	ITEM_LIMIT_NORMAL_DUNGEON = 27,
	ITEM_LIMIT_DAILY_TASK = 28,
	ITEM_LIMIT_DAILY_TASK_SCORE = 29,
	ITEM_LIMIT_RAND_TASK_DROP = 30,
	ITEM_LIMIT_EXPEDITION = 31,
	ITEM_LIMIT_SMALL_MONSTER_DIE = 32,
	ITEM_LIMIT_ELITE_MONSTER_DIE = 33,
	ITEM_LIMIT_BOSS_MONSTER_DIE = 34,
	ITEM_LIMIT_BIG_BOSS_MONSTER_DIE = 35,
	ITEM_LIMIT_SMALL_ENV_ANIMAL_DIE = 36,
	ITEM_LIMIT_MONSTER_EXCEL_DROP = 37,
	ITEM_LIMIT_REPEATABLE_QUEST_REWARD = 38,
	ITEM_LIMIT_ENV_ANIMAL = 39,
	ITEM_LIMIT_GATHER = 40,
	ITEM_LIMIT_OPERATION_DAILY_ACTIVITY = 41,
	ITEM_LIMIT_QUEST_ADD_ITEM = 42,
	ITEM_LIMIT_BONUS_ACTIVITY = 43,
	ITEM_LIMIT_TOWER_MONTHLY = 44,
	ITEM_LIMIT_BATTLE_PASS_LEVEL_REWARD = 45,
	ITEM_LIMIT_SIGN_IN = 46,
	ITEM_LIMIT_GACHA = 47,
	ITEM_LIMIT_SHOP = 48,
	ITEM_LIMIT_COOK = 49,
	ITEM_LIMIT_COMPOUND = 50,
	ITEM_LIMIT_COMBINE = 51,
	ITEM_LIMIT_FORGE = 52,
	ITEM_LIMIT_RANDOM_CHEST = 53,
	ITEM_LIMIT_USE_ITEM = 54,
	ITEM_LIMIT_USE_ITEM_REWARD = 55,
	ITEM_LIMIT_BLOSSOM_CHEST_DROP = 56,
	ITEM_LIMIT_MP_PLAY_DROP = 57,
	ITEM_LIMIT_FORGE_DROP = 58,
	ITEM_LIMIT_MP_PLAY_CRUCIBLE_DROP = 59,
	ITEM_LIMIT_ACTIVITY_CRUCIBLE_REWARD = 60,
	ITEM_LIMIT_ACTIVITY_REWARD = 61,
	ITEM_LIMIT_ACTIVITY_TRIAL_AVATAR = 62,
	ITEM_LIMIT_ACTIVITY_SALESMAN = 63,
	ITEM_LIMIT_REBATE = 64,
	ITEM_LIMIT_MONTH_CARD = 65,
	ITEM_LIMIT_BIRTHDAY_BENEFIT = 67,
	ITEM_LIMIT_ACHIEVEMENT_REWARD = 68,
	ITEM_LIMIT_ACHIEVEMENT_GOAL_REWARD = 69,
	ITEM_LIMIT_DESTROY_RETURN_MATERIAL = 70,
	ITEM_LIMIT_TOWER_REWARD = 71,
	ITEM_LIMIT_SHARE_REWARD = 72,
	ITEM_LIMIT_FETTER_LEVEL_REWARD = 73,
	ITEM_LIMIT_DUNGEON_FIRST_PASS_REWARD = 74,
	ITEM_LIMIT_CODEX_LEVELUP_REWARD = 75,
	ITEM_LIMIT_ACTIVITY_DELIVERY = 76,
	ITEM_LIMIT_CITY_REPUTATION_LEVEL = 77,
	ITEM_LIMIT_CITY_REPUTATION_QUEST = 78,
	ITEM_LIMIT_UPGRADE_WEAPON_RETURN_MATERIAL = 79,
	ITEM_LIMIT_CITY_REPUTATION_REQUEST = 80,
	ITEM_LIMIT_HUNTING_REWARD = 81,
	ITEM_LIMIT_CITY_REPUTATION_EXPLORE = 82,
	ITEM_LIMIT_ACTIVITY_FLIGHT_REWARD = 83,
	ITEM_LIMIT_ACTIVITY_ASTER_REWARD = 84,
	ITEM_LIMIT_ACTIVITY_ASTER_DROP = 85,
	ITEM_LIMIT_ACTIVITY_DRGAON_SPINE_REWARD = 86,
	ITEM_LIMIT_ACTIVITY_DRAGON_SPINE_DROP = 87,
	ITEM_LIMIT_SNOW_MOUNTAIN_TREE_TASK = 88,
	ITEM_LIMIT_SNOW_MOUNTAIN_TREE_LEVEL = 89,
	ITEM_LIMIT_ACTIVITY_SALESMAN_CHEST_DROP = 90,
	ITEM_LIMIT_ACTIVITY_EFFIGY_REWARD = 91,
	ITEM_LIMIT_REUNION_REWARD = 92,
	ITEM_LIMIT_AVATAR_PROMOTE_REWARD = 93,
	ITEM_LIMIT_ACTIVITY_SALESMAN_MP = 94,
	ITEM_LIMIT_ACTIVITY_TREASURE_MAP_REWARD = 95,
	ITEM_LIMIT_RESIN_CARD = 96,
	ITEM_LIMIT_ACTIVITY_BLESSING = 97,
	ITEM_LIMIT_MIRACLE_RING_REWARD = 98,
	ITEM_LIMIT_ACTIVITY_EXPEDITION_REWARD = 99,
	ITEM_LIMIT_MECHANICUS_BATTLE_SETTLE_REWARD = 100,
	ITEM_LIMIT_SEA_LAMP_ACTIVITY_AVATAR_REWARD = 101,
	ITEM_LIMIT_PLAYER_EXP_OVERFLOW_TRANSFORM = 1401,
	ITEM_LIMIT_ACTIVITY_ARENA_CHALLENGE_REWARD = 1402,
	ITEM_LIMIT_COOP_TASK_REWARD = 1403,
	ITEM_LIMIT_WATER_SPIRIT_REWARD = 1404,
	ITEM_LIMIT_REGION_SEARCH_REWARD = 1405,
	ITEM_LIMIT_WATER_SPIRIT_DROP = 1406,
	ITEM_LIMTT_ACTIVITY_FLEUR_FAIR_REWARD = 1407,
	ITEM_LIMIT_HIT_TREE_DROP = 1501,
	ITEM_LIMIT_ACTIVITY_MIST_TRIAL = 1502,
	ITEM_LIMIT_ACTIVITY_CHANNELLER_SLAB = 1503,
	ITEM_LIMIT_ACTIVITY_HIDE_AND_SEEK = 1504,
	ITEM_LIMIT_ACTIVITY_FIND_HILICHURL = 1505,
	ITEM_LIMIT_FURNITURE_MAKE_CANCEL = 1506,
	ITEM_LIMIT_HOME_LEVELUP_REWARD = 1507,
	ITEM_LIMIT_FURNITURE_MAKE = 1508,
	ITEM_LIMIT_ACTIVITY_CONDITION_MAIL_REWARD = 1509,
	ITEM_LIMIT_BATTLE_PASS_NOTIFY_MAIL_REWARD = 1510,
	ITEM_LIMIT_ACTIVITY_CHANNELLER_SLAB_AVATAR_REWARD = 1511,
	ITEM_LIMIT_HOME_RESOURCE = 1512,
	ITEM_LIMIT_OPERATION_WELFARE = 1513,
	ITEM_LIMIT_ACTIVITY_SUMMER_TIME_REWARD = 1601,
	ITEM_LIMIT_ACTIVITY_ECHO_SHELL = 1602,
	ITEM_LIMIT_RELIQUARY_DECOMPOSE = 1603,
	ITEM_LIMIT_HOME_FETTER = 1604,
	ITEM_LIMIT_HOME_EVENT_REWARD = 1605,
	ITEM_LIMIT_ACTIVITY_BUOYANT_COMBAT_REWARD = 1606,
	ITEM_LIMIT_ACTIVITY_BOUNCE_CONJURING_REWARD = 1607,
	ITEM_LIMIT_ORAIONOKAMI_REWARD = 2000,
	ITEM_LIMIT_BOOK_LEAF_DROP = 2001,
	ITEM_LIMIT_EXPLORATION_LINE_LEVEL_REWARD = 2002,
	ITEM_LIMIT_ACTIVITY_BLITZ_RUSH_REWARD = 2003,
	ITEM_LIMIT_ACTIVITY_BLITZ_RUSH_AVATAR_REWARD = 2004,
	ITEM_LIMIT_ACTIVITY_CHESS_REWARD = 2005,
	ITEM_LIMIT_HOME_PLANT_BOX_SEED_DROP = 2006,
	ITEM_LIMIT_HOME_PLANT_GATHER_DROP = 2007,
	ITEM_LIMIT_HOME_FURNITURE_MAKE_RETURN = 2008,
	ITEM_LIMIT_ACTIVITY_SUMO = 2100,
	ITEM_LIMIT_ACTIVITY_LUNA_RITE = 2101,
	ITEM_LIMIT_FISH_REWARD = 2102,
	ITEM_LIMIT_ACTIVITY_MOONFIN_TRIAL = 2103,
	ITEM_LIMIT_ACTIVITY_LUNA_RITE_DROP = 2104,
	ITEM_LIMIT_ACTIVITY_ROGUELIKE_DUNGEON = 2200,
	ITEM_LIMIT_ACTIVITY_ROGUELIKE_DUNGEON_AVATAR_REWARD = 2201,
	ITEM_LIMIT_ACTIVITY_PLANT_FLOWER = 2202,
	ITEM_LIMIT_ACTIVITY_DIG_REWARD = 2203,
	ITEM_LIMIT_ACTIVITY_DIG_DROP = 2204,
	ITEM_LIMIT_ACTIVITY_MUSIC_GAME_REWARD = 2205,
	ITEM_LIMIT_CAPTURE_REWARD = 2300,
	ITEM_LIMIT_WINTER_CAMP_REWARD = 2301,
	ITEM_LIMIT_WINTER_CAMP_DROP = 2302,
	ITEM_LIMIT_CAPTURE_NONE_DROP = 2303,
	ITEM_LIMIT_ACTIVITY_HACHI_REWARD = 2304,
	ITEM_LIMIT_ACTIVITY_POTION = 2400,
	ITEM_LIMIT_ACTIVITY_TANUKI_TRAVEL = 2401,
	ITEM_LIMIT_ACTIVITY_LANTERN_RITE = 2402,
	ITEM_LIMIT_ACTIVITY_LANTERN_RITE_AVATAR_REWARD = 2403,
	ITEM_LIMIT_ACTIVITY_LANTERN_RITE_DROP = 2404,
	ITEM_LIMIT_ACTIVITY_MICHIAE_MATSURI = 2500,
	ITEM_LIMIT_ACTIVITY_MICHIAE_MATSURI_DROP = 2501,
	ITEM_LIMIT_ACTIVITY_BARTENDER = 2502,
	ITEM_LIMIT_UGC_REWARD = 2503,
	ITEM_LIMIT_LUMEN_STONE_LEVEL = 2600,
	ITEM_LIMIT_ACTIVITY_CRYSTAL_LINK = 2601,
	ITEM_LIMIT_ACTIVITY_SPICE = 2602,
	ITEM_LIMIT_ACTIVITY_IRODORI = 2603,
	ITEM_LIMIT_ACTIVITY_IRODORI_AVATAR_REWARD = 2604,
	ITEM_LIMIT_ACTIVITY_PHOTO = 2605,
	ITEM_LIMIT_ACTIVITY_GACHA = 2700,
	ITEM_LIMIT_ACTIVITY_ROGUE_DIARY = 2701,
	ITEM_LIMIT_COMEBACK_QUESTIONNAIRE = 2702,
	ITEM_LIMIT_LUMINANCE_STONE_CHALLENGE = 2703,
	ITEM_LIMIT_GIVING_TAKE_BACK = 2800,
	ITEM_LIMIT_ACTIVITY_SUMMER_TIME_V2 = 2801,
	ITEM_LIMIT_ACTIVITY_SUMMER_TIME_V2_AVATAR_REWARD = 2802,
	ITEM_LIMIT_ACTIVITY_ISLAND_PARTY = 2803,
	ITEM_LIMIT_ACTIVITY_GEAR = 2804,
}