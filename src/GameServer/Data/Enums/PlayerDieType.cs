
namespace Weedwacker.GameServer.Data.Enums;

public enum PlayerDieType : int
{
	PLAYER_DIE_NONE = 0,
	PLAYER_DIE_KILL_BY_MONSTER = 1,
	PLAYER_DIE_KILL_BY_GEAR = 2,
	PLAYER_DIE_FALL = 3,
	PLAYER_DIE_DRAWN = 4,
	PLAYER_DIE_ABYSS = 5,
}