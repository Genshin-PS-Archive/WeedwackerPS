namespace Weedwacker.GameServer.Data;

public class ConfigAudioEventCulling
{
	public Dictionary<string, ConfigAudioEventCullingRuleBase[]> ruleMap;
	public ConfigAudioEventCullingRuleBase[] globalRules;
}