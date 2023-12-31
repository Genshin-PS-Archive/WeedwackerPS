
namespace Weedwacker.GameServer.Data.Enums;

public enum RogueGadgetStateType : int
{
	ROGUE_GADGET_STATE_NONE = 0,
	ROGUE_GADGET_STATE_DOOR_OPEN = 1,
	ROGUE_GADGET_STATE_DOOR_CLOSE = 2,
	ROGUE_GADGET_STATE_OPERATOR_DEFAULT = 3,
	ROGUE_GADGET_STATE_OPERATOR_IN_CHALLENGE = 4,
	ROGUE_GADGET_STATE_OPERATOR_FINISH_CHALLENGE = 5,
	ROGUE_GADGET_STATE_OPERATOR_AFTER_USE = 6,
	ROGUE_GADGET_STATE_OPERATOR_FORBID = 7,
}