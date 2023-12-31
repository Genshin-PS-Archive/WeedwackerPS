
namespace Weedwacker.GameServer.Data.Enums;

public enum BoardEventType : int
{
	BOARD_EVENT_NONE = 0,
	BOARD_EVENT_KILL_MONSTER = 1,
	BOARD_EVENT_GAME = 101,
	BOARD_EVENT_GATHER = 102,
	BOARD_EVENT_ACCELERATE = 104,
	BOARD_EVENT_USE_ITEM = 105,
	BOARD_EVENT_CHANGE_AVATAR = 106,
	BOARD_EVENT_SKILL = 107,
	BOARD_EVENT_REGION = 108,
}