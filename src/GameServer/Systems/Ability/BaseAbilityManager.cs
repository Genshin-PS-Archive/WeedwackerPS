using Google.Protobuf;
using Weedwacker.GameServer.Data;
using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Data.BinOut.Shared.ConfigEntity;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Network.Proto;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Systems.Ability;

public abstract class BaseAbilityManager
{
    protected record ElementDurabilityInfo(AbilityMetaModifierDurabilityChange Durability, long LastTickTime);

    public readonly BaseEntity Owner;
    protected Dictionary<uint, uint> InstanceToAbilityHashMap = new(); // <instancedAbilityId, abilityNameHash>
    // Maps instanced modifiers to their originally owned ability.
    // This entity cannot guarantee to be the owner or know the owner of an instanced modifier.
    protected Dictionary<uint, AbilityInstance?> InstanceToModifierAbilityMap = new(); // <instancedModifierId, abilityInstance>
                                                                                      //TODO AchievementData.txt add abilityGroups
                                                                                      //TODO ConfigGlobalCombat.json adds default abilities
    protected abstract Dictionary<uint, ConfigAbility> ConfigAbilityHashMap { get; } // <abilityNameHash, configAbility>

    public readonly Dictionary<uint, Dictionary<uint, float>> AbilitySpecialOverrideMap = new(); // <abilityNameHash, <abilitySpecialHash, value>>
    public abstract Dictionary<uint, Dictionary<uint, object>?>? AbilitySpecials { get; }// <abilityNameHash, <abilitySpecialHash, value>>
    public abstract Dictionary<string, HashSet<string>> UnlockedTalentParams { get; }

    protected Dictionary<uint, float> GlobalValueHashMap = new(); // <hash, value> TODO map the hashes to variable names

    protected BaseAbilityManager(BaseEntity owner)
    {
        Owner = owner;
    }

    public virtual void Initialize()
    {
        foreach (KeyValuePair<uint, Dictionary<uint, object>> ability in AbilitySpecials)
        {
            uint ablHash = ability.Key;
            AbilitySpecialOverrideMap[ablHash] = new();
            if (ability.Value != null)
            {
                foreach (KeyValuePair<uint, object> special in ability.Value)
                {
                    AbilitySpecialOverrideMap[ablHash][special.Key] = DynamicFloatHelper.ResolveDynamicFloat(special.Value, Owner, special.Key);
                }
            }
        }
    }


    internal virtual async Task HandleAbilityInvokeAsync(AbilityInvokeEntry invoke, Player.Player player)
    {
        if (invoke.Head?.LocalId != 0)
        {
            if (GameServer.Configuration.Server.LogOptions is { LogAbilityInvocations: true, LogAbilities: AbilityDebugMode.INFO or AbilityDebugMode.WARN or AbilityDebugMode.ERROR or AbilityDebugMode.ALL })
                Logger.WriteLine($"Ability invoke {invoke.ArgumentType} ({(int)invoke.ArgumentType}) on {player.Scene.GetEntityById(invoke.EntityId).GetType().Name} ({invoke.EntityId})");

            await HandleServerInvoke(invoke, player);
            return;
        }

        IBufferMessage info = new AbilityMetaModifierChange();
        ByteString data = invoke.AbilityData;

        //TODO add all cases
        switch (invoke.ArgumentType)
        {
            case AbilityInvokeArgument.MetaModifierChange:
                info = AbilityMetaModifierChange.Parser.ParseFrom(data);
                ChangeModifier(info as AbilityMetaModifierChange, invoke, player);
                break;
            case AbilityInvokeArgument.MetaSpecialFloatArgument:
                info = AbilityMetaSpecialFloatArgument.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MetaOverrideParam:
                info = AbilityScalarValueEntry.Parser.ParseFrom(data);
                AbilityScalarValueEntry? asEntri = info as AbilityScalarValueEntry;
                AbilitySpecialOverrideMap[InstanceToAbilityHashMap[invoke.Head.InstancedAbilityId]][asEntri.Key.Hash] = asEntri.FloatValue;
                break;
            case AbilityInvokeArgument.MetaReinitOverridemap:
                info = AbilityMetaReInitOverrideMap.Parser.ParseFrom(data);
                ReInitOverrideMap(InstanceToAbilityHashMap[invoke.Head.InstancedAbilityId], info as AbilityMetaReInitOverrideMap);
                break;
            case AbilityInvokeArgument.MetaGlobalFloatValue:
                info = AbilityScalarValueEntry.Parser.ParseFrom(data);
                AbilityScalarValueEntry? asEntry = info as AbilityScalarValueEntry;
                GlobalValueHashMap[asEntry.Key.Hash] = asEntry.FloatValue;
                break;
            case AbilityInvokeArgument.MetaAddOrGetAbilityAndTrigger:
                info = AbilityMetaAddOrGetAbilityAndTrigger.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MetaAddNewAbility:
                info = AbilityMetaAddAbility.Parser.ParseFrom(data);
                AddAbility((info as AbilityMetaAddAbility).Ability);
                break;
            case AbilityInvokeArgument.MetaModifierDurabilityChange:
                info = AbilityMetaModifierDurabilityChange.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MetaElementReactionVisual:
                info = AbilityMetaElementReactionVisual.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MetaUpdateBaseReactionDamage:
                info = AbilityMetaUpdateBaseReactionDamage.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MetaTriggerElementReaction:
                info = AbilityMetaTriggerElementReaction.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MetaLoseHp:
                info = AbilityMetaLoseHp.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.ActionTriggerAbility:
                info = AbilityActionTriggerAbility.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.ActionGenerateElemBall:
                info = AbilityActionGenerateElemBall.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.ActionFireAfterImage:
                info = AbilityActionFireAfterImage.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MixinWindZone:
                info = AbilityMixinWindZone.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MixinCostStamina:
                info = AbilityMixinCostStamina.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MixinGlobalShield:
                info = AbilityMixinGlobalShield.Parser.ParseFrom(data);
                break;
            case AbilityInvokeArgument.MixinWindSeedSpawner:
                info = AbilityMixinWindSeedSpawner.Parser.ParseFrom(data);
                break;
            default:
                Logger.DebugWriteLine($"Unhandled AbilityInvokeArgument: {invoke.ArgumentType}");
                break;
        }

#if DEBUG
        if (GameServer.Configuration.Server.LogOptions.LogAbilityInvocations)
        {
            if (!GameServer.Configuration.Server.LogOptions.AbilityInvArgBlacklist.Contains(invoke.ArgumentType))
            {
                Connection.LogAbilityInvocation("RECV ability invoke: ", invoke, info.GetType(), Owner.EntityId);
            }
        }
#endif
    }

    private async Task HandleServerInvoke(AbilityInvokeEntry invoke, Player.Player player)
    {
        // Determine ability to invoke
        AbilityInstance? ability = null;
        if (invoke.Head.InstancedModifierId != 0)
            InstanceToModifierAbilityMap.TryGetValue(invoke.Head.InstancedModifierId, out ability);

        if (ability == null && invoke.Head.InstancedAbilityId != 0 && InstanceToAbilityHashMap.TryGetValue(invoke.Head.InstancedAbilityId, out uint abilityHash))
            ability = new(ConfigAbilityHashMap[abilityHash], GetAbilityOwner(Owner));

        if (ability == null)
        {
            Logger.DebugWriteLine($"Ability not found: {invoke.Head.InstancedAbilityId} {invoke.Head.InstancedModifierId}");
            return;
        }

        // Determine target to apply ability to
        BaseEntity? targetEntity = player.Scene?.GetEntityById(invoke.Head.TargetId) ??
                                    player.Scene?.GetEntityById(invoke.EntityId);

        if (ability.Value.Data.LocalIdToInvocationMap.TryGetValue((uint)invoke.Head.LocalId, out IInvocation invocation))
            await invocation.Invoke(ability.Value, invoke.AbilityData, targetEntity);
        else
            Logger.DebugWriteLine($"Missing localId: {invoke.Head.LocalId}, ability: {invoke.Head.InstancedAbilityId}");
    }

    private BaseEntity? GetAbilityOwner(BaseEntity? owner)
    {
        if (owner is ClientGadgetEntity gadgetEntity)
            return gadgetEntity.Scene?.GetEntityById(gadgetEntity.OwnerEntityId);

        return owner;
    }

    internal void ChangeModifier(AbilityMetaModifierChange? modifierChange, AbilityInvokeEntry invoke, Player.Player player)
    {
        switch (modifierChange.Action)
        {
            case ModifierAction.Added:
                AbilityInstance? ability = null;

                if (invoke.Head.TargetId != 0)
                {
                    // Get ability from target
                    BaseEntity? targetEntity = player.Scene?.GetEntityById(invoke.Head.TargetId);

                    if (targetEntity != null)
                        if (targetEntity.AbilityManager.InstanceToAbilityHashMap.TryGetValue(invoke.Head.InstancedAbilityId, out uint abilityHash))
                            ability = new(targetEntity.AbilityManager.ConfigAbilityHashMap[abilityHash], GetAbilityOwner(targetEntity));
                }

                if (ability == null)
                {
                    // Get ability from current entity
                    if (InstanceToAbilityHashMap.TryGetValue(invoke.Head.InstancedAbilityId, out uint abilityHash))
                        ability = new(ConfigAbilityHashMap[abilityHash], GetAbilityOwner(Owner));
                }

                // Get ability from attached parent
                if (ability == null)
                {
                    if (GameData.ConfigAbilityMap.TryGetValue(modifierChange.ParentAbilityName.Str, out ConfigAbility? parentAbility))
                        ability = new(parentAbility, GetAbilityOwner(Owner));
                }

                if (ability == null)
                    Logger.DebugWriteLine($"Ability not found: {invoke.Head.InstancedAbilityId} {invoke.Head.InstancedModifierId}");

                InstanceToModifierAbilityMap[invoke.Head.InstancedModifierId] = ability;
                break;

            case ModifierAction.Removed:
                InstanceToModifierAbilityMap.Remove(invoke.Head.InstancedModifierId);
                break;
        }
    }

    protected virtual void ReInitOverrideMap(uint abilityNameHash, AbilityMetaReInitOverrideMap? overrideMap)
    {
        foreach (AbilityScalarValueEntry? entry in overrideMap.OverrideMap)
        {
            if (entry.ValueType != AbilityScalarType.Float)
            {
                Logger.DebugWriteLine($"Unhandled value type {entry.ValueType} in Config {ConfigAbilityHashMap[abilityNameHash].abilityName}");
                continue;
            }
            try
            {
                AbilitySpecialOverrideMap[abilityNameHash][entry.Key.Hash] = entry.FloatValue;
            }
            catch
            {
                AbilitySpecialOverrideMap[abilityNameHash] = new();
                AbilitySpecialOverrideMap[abilityNameHash][entry.Key.Hash] = entry.FloatValue;
            }
        }
    }

    protected void AddAbility(AbilityAppliedAbility ability)
    {
        uint hash = ability.AbilityName.Hash;
        uint instancedId = ability.InstancedAbilityId;
        InstanceToAbilityHashMap[instancedId] = hash;
        if (ability.OverrideMap.Any())
        {
            foreach (AbilityScalarValueEntry? entry in ability.OverrideMap)
            {
                switch (entry.ValueType)
                {
                    case AbilityScalarType.Float:
                        if (AbilitySpecialOverrideMap.TryGetValue(hash, out Dictionary<uint, float> specials))
                        {
                            specials[entry.Key.Hash] = entry.FloatValue;
                        }
                        else
                        {
                            AbilitySpecialOverrideMap[hash] = new() { [entry.Key.Hash] = entry.FloatValue };
                        }
                        break;
                    default:
                        Logger.WriteErrorLine($"Unhandled value type {entry.ValueType} in Config {ConfigAbilityHashMap[hash].abilityName}");
                        break;
                }
            }
        }
    }

    public virtual void AddAbility(string abilityName)
    {
        uint ablHash = Utils.AbilityHash(abilityName);
        ConfigAbility config = GameData.ConfigAbilityMap[abilityName];
        ConfigAbilityHashMap[ablHash] = config;
        if (config.abilitySpecials != null)
        {
            AbilitySpecials.Add(ablHash, config.abilitySpecials.ToDictionary(w => Utils.AbilityHash(w.Key), w => w.Value));
            AbilitySpecials[ablHash] = new Dictionary<uint, object>();
            AbilitySpecialOverrideMap[ablHash] = new Dictionary<uint, float>();
            foreach (KeyValuePair<string, object> special in config.abilitySpecials)
            {
                if (special.Value == null) continue;
                object? specialVal = special.Value;
                uint specialHash = Utils.AbilityHash(special.Key);
                AbilitySpecials[ablHash][specialHash] = specialVal;
                AbilitySpecialOverrideMap[ablHash][specialHash] = DynamicFloatHelper.ResolveDynamicFloat(specialVal, Owner, special.Key);
            }
        }
    }

    protected void AddConfigEntityAbilityEntries(IEnumerable<ConfigEntityAbilityEntry> entries)
    {
        foreach (ConfigEntityAbilityEntry abilityEntry in entries)
        {
            ConfigAbility ability = GameData.ConfigAbilityMap[abilityEntry.abilityName];
            uint abilityHash = Utils.AbilityHash(abilityEntry.abilityName);
            ConfigAbilityHashMap.Add(abilityHash, ability);
            if (ability.abilitySpecials != null)
            {
                AbilitySpecials[abilityHash] = new Dictionary<uint, object>();
                foreach (KeyValuePair<string, object> special in ability.abilitySpecials)
                {
                    AbilitySpecials[abilityHash][Utils.AbilityHash(special.Key)] = special.Value;
                }
            }
        }
    }
}
