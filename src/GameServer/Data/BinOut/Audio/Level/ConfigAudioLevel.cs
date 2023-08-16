using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigAudioLevel
{
	public ConfigWwiseString[] teleportEvents;
	public ConfigWwiseString stateGroup;
	public Dictionary<string, ConfigWwiseString> dungeonEventMap;
}