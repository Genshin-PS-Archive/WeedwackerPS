using Weedwacker.GameServer.Data.BinOut.Ability.Temp;
using Weedwacker.GameServer.Data.BinOut.Talent;
using Weedwacker.GameServer.Data.Excel;
using Weedwacker.GameServer.Systems.Avatar;
using Weedwacker.GameServer.Systems.World;
using Weedwacker.Shared.Network.Proto;

namespace Weedwacker.GameServer.Systems.Ability
{
	internal class AvatarAbilityManager : BaseAbilityManager
	{
#pragma warning disable CS8603 // Possible null reference return.
		private AvatarEntity AvatarOwner => Owner as AvatarEntity;
#pragma warning restore CS8603 // Possible null reference return.
		private SkillDepot CurDepot => AvatarOwner.Avatar.CurSkillDepot;
		private uint CurDepotId => CurDepot.DepotId;
		protected override Dictionary<uint, ConfigAbility> ConfigAbilityHashMap => CurDepot.Abilities;
		public override Dictionary<uint, Dictionary<uint, object>?>? AbilitySpecials => CurDepot.AbilitySpecials;
		public override Dictionary<string, HashSet<string>> UnlockedTalentParams => CurDepot.UnlockedTalentParams;


		public AvatarAbilityManager(AvatarEntity avatar) : base(avatar)
		{
		}

		public override void Initialize()
		{
			foreach (ProudSkillData? proudSkill in CurDepot.InherentProudSkillOpens)
			{
				if (proudSkill.openConfig == null || proudSkill.openConfig == "") continue;
				foreach (ConfigTalentMixin config in AvatarOwner.Avatar.Data.ConfigTalentMap[AvatarOwner.Avatar.CurrentDepotId][proudSkill.openConfig])
				{
					config.Apply(this, proudSkill.paramList.Cast<float?>().ToArray());
				}
			}
			foreach (KeyValuePair<uint, uint> skill in CurDepot.Skills)
			{
				AvatarSkillData skillData = AvatarOwner.Avatar.Data.SkillData[CurDepotId][skill.Key];
				ProudSkillData proudSkill = AvatarOwner.Avatar.Data.ProudSkillData[CurDepotId]
					.Where(w => w.Value.proudSkillGroupId == skillData.proudSkillGroupId && w.Value.level == skill.Value).First().Value;
				if (AvatarOwner.Avatar.Data.ConfigTalentMap.ContainsKey(CurDepotId))
				{
					foreach (ConfigTalentMixin config in AvatarOwner.Avatar.Data.ConfigTalentMap[CurDepotId][proudSkill.openConfig])
					{
						config.Apply(this, proudSkill.paramList.Cast<float?>().ToArray());
					}
				}
			}
			if (CurDepot.Element != null)
			{
				uint energySkill = (uint)CurDepot.EnergySkill;
				uint energySkillLevel = CurDepot.EnergySkillLevel;
				AvatarSkillData skillData = AvatarOwner.Avatar.Data.SkillData[CurDepotId][energySkill];
				ProudSkillData proudSkill = AvatarOwner.Avatar.Data.ProudSkillData[CurDepotId]
					.Where(w => w.Value.proudSkillGroupId == skillData.proudSkillGroupId && w.Value.level == energySkillLevel).First().Value;
				foreach (ConfigTalentMixin config in AvatarOwner.Avatar.Data.ConfigTalentMap[CurDepotId][proudSkill.openConfig])
				{
					//config.Apply(this, proudSkill.paramList.Cast<float?>().ToArray());
				}
			}
			base.Initialize();
		}
	}
}
