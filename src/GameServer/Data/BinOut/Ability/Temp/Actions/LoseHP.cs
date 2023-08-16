using Google.Protobuf;
using Newtonsoft.Json;
using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Enums;
using Weedwacker.GameServer.Systems.Ability;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Data.BinOut.Ability.Temp.Actions;

public class LoseHP : ConfigAbilityAction
{
    public object? amount;
    public object? amountByCasterMaxHPRatio;
    public object? amountByTargetMaxHPRatio;
    public object? amountByTargetCurrentHPRatio;
    public object? amountByCasterAttackRatio;
    public object? limboByTargetMaxHPRatio;
    public bool lethal;
    public bool enableInvincible;
    public bool enableLockHP;
    public bool disableWhenLoading;
    public bool useMeta;

    public override async Task Invoke(AbilityInstance ability, ByteString abilityData, BaseEntity? targetEntity)
    {
        if (ability.Owner == null || targetEntity == null) return;
        if (ability.Owner is not SceneEntity sceneOwner || targetEntity is not SceneEntity sceneTarget) return;
        if (enableLockHP && sceneTarget.LockHP) return;
        if (disableWhenLoading && sceneTarget.Scene.World.Host.SceneLoadState != SceneLoadState.LOADED) return;

        float limboRatio = 0f;
        if (limboByTargetMaxHPRatio != null)
        {
            limboRatio = DynamicFloatHelper.ResolveDynamicFloat(limboByTargetMaxHPRatio, targetEntity, ability.Data.abilityName);
            if (limboRatio == 0f) return;
        }

        float loseAmount = GetLoseAmount(ability.Data, sceneOwner, sceneTarget, limboRatio);

        await sceneTarget.DamageAsync(loseAmount);
    }

    private float GetLoseAmount(ConfigAbility ability, SceneEntity abilityOwner, SceneEntity targetEntity, float limboRatio)
    {
        Logger.DebugWriteLine($"{JsonConvert.SerializeObject(this)}");

        float loseAmount = 0;

        if (amount != null)
            loseAmount += DynamicFloatHelper.ResolveDynamicFloat(amount, abilityOwner, ability.abilityName);

        if (amountByCasterMaxHPRatio != null)
            loseAmount += abilityOwner.FightProps[FightPropType.FIGHT_PROP_MAX_HP] * DynamicFloatHelper.ResolveDynamicFloat(amountByCasterMaxHPRatio, abilityOwner, ability.abilityName);

        if (amountByCasterAttackRatio != null)
            loseAmount += abilityOwner.FightProps[FightPropType.FIGHT_PROP_CUR_ATTACK] * DynamicFloatHelper.ResolveDynamicFloat(amountByCasterAttackRatio, abilityOwner, ability.abilityName);

        float maxHp = targetEntity.FightProps[FightPropType.FIGHT_PROP_MAX_HP];
        if (amountByTargetMaxHPRatio != null)
            loseAmount += maxHp * DynamicFloatHelper.ResolveDynamicFloat(amountByTargetMaxHPRatio, targetEntity, ability.abilityName);

        float curHp = targetEntity.FightProps[FightPropType.FIGHT_PROP_CUR_HP];
        if (amountByTargetCurrentHPRatio != null)
            loseAmount += curHp * DynamicFloatHelper.ResolveDynamicFloat(amountByTargetCurrentHPRatio, targetEntity, ability.abilityName);

        if (limboRatio > 1.192093e-07)
            loseAmount = Math.Min(Math.Max(curHp - Math.Max(limboRatio * maxHp, 1), 0), loseAmount);

        if (curHp < loseAmount + .01 && !lethal)
            loseAmount = 0;

        return loseAmount;
    }
}
