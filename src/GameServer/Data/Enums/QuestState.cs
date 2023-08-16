
namespace Weedwacker.GameServer.Data.Enums;

public enum QuestState : int
{
	QUEST_STATE_NONE = 0,
	QUEST_STATE_UNSTARTED = 1,
	QUEST_STATE_UNFINISHED = 2,
	QUEST_STATE_FINISHED = 3,
	QUEST_STATE_FAILED = 4,

	// Used by lua
	NONE = 0,
	UNSTARTED = 1,
	UNFINISHED = 2,
	FINISHED = 3,
	FAILED = 4
}