using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigAudioAvatar
{
	public ConfigWwiseString voiceSwitchKey;
	public ConfigWwiseString bodyTypeSwitchKey;
	public string[] weaponPutAwayAnimStateWhitelist;
	public ConfigWwiseString energyRatioRtpc;
	public ConfigWwiseString teamHpRTPC;
	public float teamHpAliveRatio;
	public ConfigWwiseString avatarAccessTypeSwitchGroup;
	public ConfigWwiseString localAvatarSwitchValue;
	public ConfigWwiseString remoteAvatarSwitchValue;
	public ConfigWwiseString motionAvatarTypeRtpcKey;
}