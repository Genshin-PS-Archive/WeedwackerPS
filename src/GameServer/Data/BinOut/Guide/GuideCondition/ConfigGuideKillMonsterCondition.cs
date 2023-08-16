using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigGuideKillMonsterCondition : ConfigGuideCondition
{
	public bool spawnNew;
	public uint monsterID;
	public uint monsterLevel;
	public Vector monsterPos;
	public float monsterYaw;
}