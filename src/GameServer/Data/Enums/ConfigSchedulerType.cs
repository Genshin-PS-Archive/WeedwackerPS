
namespace Weedwacker.GameServer.Data.Enums;

public enum ConfigSchedulerType : int
{
	I_UPDATE = 0,
	I_LATE_PRE_UPDATE = 1,
	I_LATE_POST_UPDATE = 2,
	I_AFTER_RES_TO_UPDATE_END = 3,
	I_LATE_POST_UPDATE_TO_NEXT_BEFORE_LATE_UPDATE = 4,
	I_RENDER_TO_NEXT_BEFORE_RES = 5,
	I_RENDER_TO_NEXT_BEFORE_LATE_UPDATE = 6,
	I_RENDER_TO_NEXT_BEFORE_NETWORK = 7,
	I_AFTER_NETWORK_TO_RENDER = 8,
	I_AFTER_ENTITY_TO_EFFECT_END = 9,
	I_AFTER_LATE_UPDATE_POST = 10,
	G_RENDER_TO_NEXT_BEFORE_LATE_UPDATE = 100,
}