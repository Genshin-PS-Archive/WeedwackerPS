using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Systems.Player;

internal class ExpManager
{
	private readonly Player Owner;
	public ExpManager(Player owner)
	{
		Owner = owner;
	}
	public async Task<bool> AddAdventureExp(int exp)
	{
		//TODO
		return await Owner.PropManager.SetPropertyAsync(PropType.PROP_EXP, Owner.PlayerProperties[PropType.PROP_EXP] + exp);
	}

	public async Task<bool> AddCharacterExp(Avatar.Avatar avatar, int exp)
	{
		//TODO
		return true;
	}

	public async Task<bool> AddCompanionshipExp(int exp)
	{
		//TODO
		return true;
	}
}
