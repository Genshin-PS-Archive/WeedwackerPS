using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigCrowdSpawnInfo
{
	public int crowdTypeID;
	public int animConfigID;
	public Vector spawnPosition;
	public Vector spawnFaceFwd;
	public bool hasCollider;
	public bool sitOnChair;
	public ColorVector hairColor;
	public ColorVector[] bodyColors;
	public int instanceId;
}