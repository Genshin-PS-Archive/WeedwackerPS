namespace Weedwacker.GameServer.Data;

public class ConfigGuideDoActionByPredicate : ConfigGuideAction
{
	public ConfigGuideCondition[][] predicates;
	public ConfigGuideAction[] actions;
}