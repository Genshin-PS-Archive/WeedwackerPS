using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigEmotionInterAction : ConfigBaseInterAction
{
	public string emotionName;
	public float emotionTransitionTime;
	public Vector lookAtOffset;
	public bool blinkEnabled;
	public float blinkMinGap;
	public float blinkMaxGap;
	public float blinkDuration;
}