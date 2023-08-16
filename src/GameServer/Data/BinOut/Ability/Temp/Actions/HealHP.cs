using Google.Protobuf;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Systems.Ability;
using Weedwacker.GameServer.Systems.World;

namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp.Actions;

public class HealHP : ConfigAbilityAction
{
    public object? amount;
    public object? amountByCasterMaxHPRatio;
    public object? amountByTargetMaxHPRatio;
    public object? amountByTargetCurrentHPRatio;
    public object? amountByCasterAttackRatio;
    public bool muteHealEffect;
    public float healRatio = 1f;
    public bool ignoreAbilityProperty;

    public override async Task Invoke(AbilityInstance ability, ByteString abilityData, BaseEntity? targetEntity)
    {
        if (ability.Owner == null || targetEntity == null)
            return;

        if(ability.Owner is not SceneEntity sceneOwner || targetEntity is not SceneEntity sceneTarget) 
            return;

        // Get healing amount
        float? healAmount = GetHealAmount(ability.Data, sceneOwner, sceneTarget);
        if (healAmount == null)
            return;

        await sceneTarget.HealAsync(healAmount.Value, muteHealEffect);
    }

    private float? GetHealAmount(ConfigAbility ability, SceneEntity abilityOwner, SceneEntity targetEntity)
    {
        float healAmount = 0;
        float abilityRatio = 1f;

        if (amount != null)
            healAmount += DynamicFloatHelper.ResolveDynamicFloat(amount, abilityOwner, ability.abilityName);

        if (amountByCasterMaxHPRatio != null)
            healAmount += abilityOwner.FightProps[FightPropType.FIGHT_PROP_MAX_HP] * DynamicFloatHelper.ResolveDynamicFloat(amountByCasterMaxHPRatio, abilityOwner, ability.abilityName);

        if (amountByCasterAttackRatio != null)
            healAmount += abilityOwner.FightProps[FightPropType.FIGHT_PROP_CUR_ATTACK] * DynamicFloatHelper.ResolveDynamicFloat(amountByCasterAttackRatio, abilityOwner, ability.abilityName);

        if (amountByTargetMaxHPRatio != null)
            healAmount += targetEntity.FightProps[FightPropType.FIGHT_PROP_MAX_HP] * DynamicFloatHelper.ResolveDynamicFloat(amountByTargetMaxHPRatio, targetEntity, ability.abilityName);

        if (amountByTargetCurrentHPRatio != null)
            healAmount += targetEntity.FightProps[FightPropType.FIGHT_PROP_CUR_HP] * DynamicFloatHelper.ResolveDynamicFloat(amountByTargetCurrentHPRatio, targetEntity, ability.abilityName);

        if (!ignoreAbilityProperty)
        {
            targetEntity.FightProps.TryGetValue(FightPropType.FIGHT_PROP_HEAL_ADD, out float heal);
            targetEntity.FightProps.TryGetValue(FightPropType.FIGHT_PROP_HEALED_ADD, out float healed);
            abilityRatio += heal + healed;
        }

        return healAmount * abilityRatio * healRatio;
    }
}
