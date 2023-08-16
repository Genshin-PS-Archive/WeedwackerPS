using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigCameraShakeAction : ConfigBaseInterAction
{
	public float shakeRange;
	public float shakeTime;
	public float shakeDinstance;
	public Vector shakeDir;
}