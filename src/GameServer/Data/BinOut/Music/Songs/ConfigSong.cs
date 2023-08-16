using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigSong
{
	public int id;
	public ConfigWwiseString switchValue;
	public ConfigMusicSyncTransitions syncTransitions;
	public ConfigMusicTimeTransitions timeTransitions;
	public ConfigMusicStimulusHandler[] stimulusHandlers;
	public int ignoranceMask;
}