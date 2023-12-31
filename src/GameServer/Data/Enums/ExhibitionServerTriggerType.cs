
namespace Weedwacker.GameServer.Data.Enums;

public enum ExhibitionServerTriggerType : int
{
	EXHIBITION_SERVER_TRIGGER_NONE = 0,
	EXHIBITION_SERVER_TRIGGER_TEST = 1,
	EXHIBITION_SERVER_TRIGGER_HIDE_AND_SEEK_HUNTER_WIN_LAST_TIME = 2,
	EXHIBITION_SERVER_TRIGGER_HIDE_AND_SEEK_HIDER_SURVIVAL_TIME = 3,
	EXHIBITION_SERVER_TRIGGER_HIDE_AND_SEEK_WIN = 4,
	EXHIBITION_SERVER_TRIGGER_DAMAGE_BY_GROUP_CONFIGID = 5,
	EXHIBITION_SERVER_TRIGGER_DAMAGE_BY_MONSTERID = 6,
	EXHIBITION_SERVER_TRIGGER_CHESS_SUCCESS = 7,
	EXHIBITION_SERVER_TRIGGER_CHESS_SETTLE_ROUND = 8,
	EXHIBITION_SERVER_TRIGGER_CHESS_ESCAPED_MONSTERS = 9,
	EXHIBITION_SERVER_TRIGGER_CHESS_PICK_CURSE_CARD_COUNT = 10,
	EXHIBITION_SERVER_TRIGGER_CHESS_PICK_CHALLENGE_CARD_COUNT = 11,
	EXHIBITION_SERVER_TRIGGER_CHESS_OBTAIN_BUILDING_POINTS = 12,
	EXHIBITION_SERVER_TRIGGER_CHESS_PICK_SSR_CARD_COUNT = 13,
	EXHIBITION_SERVER_TRIGGER_CHESS_LAST_ROUND_KILL_STAGE_COST_TIME = 14,
	EXHIBITION_SERVER_TRIGGER_MIST_TRIAL_SHORTEST_SUCCESS_TIME = 15,
	EXHIBITION_SERVER_TRIGGER_MIST_TRIAL_STRONGEST_BLOW = 16,
	EXHIBITION_SERVER_TRIGGER_MIST_TRIAL_LEAST_TAKE_DAMAGE = 17,
	EXHIBITION_SERVER_TRIGGER_MIST_TRIAL_ELEMENT_REACTION_COUNT = 18,
	EXHIBITION_SERVER_TRIGGER_MIST_TRIAL_ELEMENT_BURST_COUNT = 19,
	EXHIBITION_SERVER_TRIGGER_MIST_TRIAL_SUCCESS = 20,
	EXHIBITION_SERVER_TRIGGER_DAMAGE_PERCENTAGE_ON_MONSTER_DIE = 21,
	EXHIBITION_SERVER_TRIGGER_DAMAGE_BY_GROUPID = 22,
	EXHIBITION_SERVER_TRIGGER_MONSTER_DRAWN_FALL_BY_TIME_GROUPID = 23,
	EXHIBITION_SERVER_TRIGGER_SUMMER_V2_DUNGEON_ELEMENT_BURST_COUNT = 24,
	EXHIBITION_SERVER_TRIGGER_GALLERY_SUCC_LAST_TIME = 25,
}