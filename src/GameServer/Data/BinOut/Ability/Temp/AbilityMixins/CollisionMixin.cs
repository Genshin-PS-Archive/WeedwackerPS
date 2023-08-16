using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp.AbilityMixins;

public class CollisionMixin : ConfigAbilityMixin
{
	public ConfigCollision collision;
	public float minShockSpeed;
	public float cd;
	public ConfigAbilityAction[] onCollision;
}
