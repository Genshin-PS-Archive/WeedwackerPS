namespace Weedwacker.GameServer.Data;

public class ConfigQTEStepNode
{
	public ConfigQTEStepCondActionGroup[] startTrigger;
	public ConfigQTEStepBaseComponent[] components;
	public ConfigQTEStepCondActionGroup[] successTrigger;
	public ConfigQTEStepCondActionGroup[] failTrigger;
}