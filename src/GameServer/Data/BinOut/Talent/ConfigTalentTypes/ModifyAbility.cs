using System.Text.RegularExpressions;
using Weedwacker.GameServer.Systems.Ability;
using Weedwacker.Shared.Utils;

namespace Weedwacker.GameServer.Data.BinOut.Talent;

internal partial class ModifyAbility : ConfigTalentMixin
{
	public string abilityName;
	public string paramSpecial;
	public object paramDelta; // plus or minus &(index in proudSkillData's or AvatarTalentData's paramList) or absolute delta
	public object paramRatio; // plus or minus &(index in proudSkillData's or AvatarTalentData's paramList) or absolute delta

	public override void Apply(BaseAbilityManager abilityManager, float?[] paramList)
	{
		if (abilityManager.AbilitySpecials == null) return;
		try
		{
			uint hash = Utils.AbilityHash(abilityName);
			if (!abilityManager.AbilitySpecials.TryGetValue(hash, out Dictionary<uint, object>? abSpecials))
			{
				abilityManager.AddAbility(abilityName);
				abSpecials = abilityManager.AbilitySpecials[hash];
			}
			object specialObj = abSpecials[Utils.AbilityHash(paramSpecial)];
			float special = DynamicFloatHelper.ResolveDynamicFloat(specialObj, abilityManager.Owner, abilityName);

			if (paramDelta is string deltaString)
			{
				string index = PercentRegex().Replace(deltaString, "");
				int paramIndex = int.Parse(index);
				float delta = paramList[Math.Abs(paramIndex)] ?? 0;
				if (paramIndex < 0)
					delta = -delta;
				special += delta;
			}
			else if (paramDelta is double asD)
				special += (float)asD;

			if (paramRatio is string ratioString)
			{
				string index = Regex.Replace(ratioString, "%", "");
				float ratio = paramList[int.Parse(index)] ?? 0;
				special *= ratio;
			}
			else if (paramRatio is double asD)
			{
				if (asD != 0)
					special *= (float)asD;
			}

			abilityManager.AbilitySpecials[Utils.AbilityHash(abilityName)][Utils.AbilityHash(paramSpecial)] = special;
		}
		catch (Exception e)
		{
			Logger.WriteErrorLine("error applying talent ModifyAbility", e);
		}
	}

	[GeneratedRegex("%")]
	private static partial Regex PercentRegex();
}
