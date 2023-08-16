
namespace Weedwacker.GameServer.Data.Enums;

public enum LogicType : int
{
	LOGIC_NONE = 0,
	LOGIC_AND = 1,
	LOGIC_OR = 2,
	LOGIC_NOT = 3,
	LOGIC_A_AND_ETCOR = 4,
	LOGIC_A_AND_B_AND_ETCOR = 5,
	LOGIC_A_OR_ETCAND = 6,
	LOGIC_A_OR_B_OR_ETCAND = 7,
	LOGIC_A_AND_B_OR_ETCAND = 8,
}