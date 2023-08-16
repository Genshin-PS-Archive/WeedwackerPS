using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data;

public class ConfigAITacticBaseSetting
{
	public bool enable;
	public ConfigAITacticCondition condition;
	public NeuronName[] nerveTrigger;
}