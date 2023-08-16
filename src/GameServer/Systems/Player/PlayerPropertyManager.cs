﻿using MongoDB.Driver;
using Weedwacker.GameServer.Database;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Packet.Send;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Systems.Player;

internal class PlayerPropertyManager
{
	public const int MIN_EXP = 0;
	public const int MIN_LEVEL = 0;
	public const int MAX_LEVEL = 90;
	public const int MIN_PLAYER_LEVEL = 1;
	public const int MAX_PLAYER_LEVEL = 60;
	public const int MIN_MAX_SPRING_VOLUME = 0;
	public const int MAX_MAX_SPRING_VOLUME = 8500000;
	public const int MIN_SPRING_AUTO_USE_PERCENT = 0;
	public const int MAX_SPRING_AUTO_USE_PERCENT = 100;
	public const int MIN_MAX_STAMINA = 0;
	public const int MAX_MAX_STAMINA = 24000;
	public const int MAX_PLAYER_RESIN = 2000;
	public const int MIN_PLAYER_SCOIN = 0;
	public const int MIN_LEGENDARY_KEY = 0;
	public const int MIN_FORGE_POINT = 0;
	public const int MAX_FORGE_POINT = 300000;
	public const int MIN_HOME_COIN = 0;

	private Player Owner;

	public PlayerPropertyManager(Player owner) { Owner = owner; }

	public async Task<bool> AddPrimogemsAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_HCOIN, Owner.PlayerProperties[PropType.PROP_PLAYER_HCOIN] + count, true, reason);
	}

	public async Task<bool> UsePrimogemsAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_HCOIN, Owner.PlayerProperties[PropType.PROP_PLAYER_HCOIN] - count, true, reason);
	}

	public async Task<bool> AddMoraAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_SCOIN, Owner.PlayerProperties[PropType.PROP_PLAYER_SCOIN] + count, true, reason);
	}

	public async Task<bool> PayMoraAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_SCOIN, Owner.PlayerProperties[PropType.PROP_PLAYER_SCOIN] - count, true, reason);
	}

	public async Task<bool> AddGenesisCrystalsAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_MCOIN, Owner.PlayerProperties[PropType.PROP_PLAYER_MCOIN] + count, true, reason);
	}

	public async Task<bool> UseGenesisCrystalsAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_MCOIN, Owner.PlayerProperties[PropType.PROP_PLAYER_MCOIN] - count, true, reason);
	}

	public async Task<bool> AddLegendaryKeyAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_LEGENDARY_KEY, Owner.PlayerProperties[PropType.PROP_PLAYER_LEGENDARY_KEY] + count, true, reason);
	}
	public async Task<bool> UseLegendaryKeyAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_LEGENDARY_KEY, Owner.PlayerProperties[PropType.PROP_PLAYER_LEGENDARY_KEY] - count, true, reason);
	}

	public async Task<bool> AddHomeCoinAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_HOME_COIN, Owner.PlayerProperties[PropType.PROP_PLAYER_HOME_COIN] + count, true, reason);
	}
	public async Task<bool> PayHomeCoinAsync(int count, PropChangeReason reason = PropChangeReason.None)
	{
		return await SetPropertyAsync(PropType.PROP_PLAYER_HOME_COIN, Owner.PlayerProperties[PropType.PROP_PLAYER_HOME_COIN] - count, true, reason);
	}

	public async Task<bool> SetPropertyAsync(PropType prop, int value, bool sendPacket = true, PropChangeReason reason = PropChangeReason.None)
	{
		switch (prop)
		{
			case PropType.PROP_EXP:
				if (value < MIN_EXP) return false;
				goto default;
			case PropType.PROP_LEVEL:
				if (value < MIN_LEVEL || value > MAX_LEVEL) return false;
				goto default;
			case PropType.PROP_MAX_SPRING_VOLUME:
				if (value < MIN_MAX_SPRING_VOLUME || value > MAX_MAX_SPRING_VOLUME) return false;
				goto default;
			case PropType.PROP_CUR_SPRING_VOLUME:
				if (value > Owner.PlayerProperties[PropType.PROP_MAX_SPRING_VOLUME]) return false;
				goto default;
			case PropType.PROP_IS_SPRING_AUTO_USE:
			case PropType.PROP_IS_FLYABLE:
			case PropType.PROP_IS_WEATHER_LOCKED:
			case PropType.PROP_IS_GAME_TIME_LOCKED:
			case PropType.PROP_IS_TRANSFERABLE:
			case PropType.PROP_IS_MP_MODE_AVAILABLE:
			case PropType.PROP_IS_ONLY_MP_WITH_PS_PLAYER:
				if (value == 0 || value == 1) goto default;
				return false;
			case PropType.PROP_SPRING_AUTO_USE_PERCENT:
				if (value < MIN_SPRING_AUTO_USE_PERCENT || value > MAX_SPRING_AUTO_USE_PERCENT) return false;
				goto default;
			case PropType.PROP_MAX_STAMINA:
				if (value < MIN_MAX_STAMINA || value > MAX_MAX_STAMINA) return false;
				goto default;
			case PropType.PROP_CUR_PERSIST_STAMINA:
				if (value > Owner.PlayerProperties[PropType.PROP_MAX_STAMINA]) return false;
				goto default;
			case PropType.PROP_PLAYER_LEVEL:
				if (value < MIN_PLAYER_LEVEL || value > MAX_PLAYER_LEVEL) return false;
				goto default;
			case PropType.PROP_PLAYER_MP_SETTING_TYPE:
				if (value < 0 || value > 2) return false;
				goto default;
			case PropType.PROP_PLAYER_WORLD_LEVEL:
				if (value < 0 || value > 8) return false;
				goto default;
			case PropType.PROP_PLAYER_RESIN:
				if (value > MAX_PLAYER_RESIN) return false;
				goto default;
			case PropType.PROP_PLAYER_SCOIN:
				if (value < MIN_PLAYER_SCOIN) return false;
				goto default;
			case PropType.PROP_PLAYER_LEGENDARY_KEY:
				if (value < MIN_LEGENDARY_KEY) return false;
				goto default;
			case PropType.PROP_PLAYER_FORGE_POINT:
				if (value < MIN_FORGE_POINT || value > MAX_FORGE_POINT) return false;
				goto default;
			case PropType.PROP_PLAYER_HOME_COIN:
				if (value < MIN_HOME_COIN) return false;
				goto default;
			default:
				int currentValue = Owner.PlayerProperties.GetValueOrDefault(prop);
				Owner.PlayerProperties[prop] = value;

				if (value - currentValue != 0)
				{
					// Update Database
					FilterDefinition<Player>? filter = Builders<Player>.Filter.Where(p => p.AccountUid == Owner.AccountUid);
					UpdateDefinition<Player>? update = Builders<Player>.Update.Set($"{nameof(Player.PlayerProperties)}.{prop}", value);
					await DatabaseManager.UpdatePlayerAsync(filter, update);

				}

				if (sendPacket)
				{
					// Update player with packet
					await Owner.SendPacketAsync(new PacketPlayerPropNotify(Owner, prop));
					await Owner.SendPacketAsync(new PacketPlayerPropChangeNotify(prop, value - currentValue));

					if (reason != PropChangeReason.None)
					{
						await Owner.SendPacketAsync(new PacketPlayerPropChangeReasonNotify(prop, currentValue, value, reason));
					}
				}
				return true;
		}

	}
}
