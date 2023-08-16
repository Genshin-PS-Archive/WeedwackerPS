using Google.Protobuf;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Ability;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Enums;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp;

public class ConfigAbilityAction : BaseActionContainer, IInvocation
{
    public ConfigAbilityActionToken token;
    public AbilityTargetting target;
    public SelectTargets otherTargets;
    public bool doOffStage;
    public bool doAfterDie;
    public bool canBeHandledOnRecover;
    public bool muteRemoteAction;
    public ConfigAbilityPredicate[] predicates;
    public ConfigAbilityPredicate[] predicatesForeach;

    public virtual async Task Invoke(AbilityInstance ability, ByteString abilityData, BaseEntity? targetEntity)
    {
#if DEBUG
        if (GameServer.Configuration.Server.LogOptions.LogAbilities is AbilityDebugMode.WARN or AbilityDebugMode.ERROR or AbilityDebugMode.ALL)
            Logger.DebugWriteWarningLine($"invoked unimplemented action: {GetType().Name}");
#endif
    }
}
