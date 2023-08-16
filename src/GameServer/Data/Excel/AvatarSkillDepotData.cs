namespace Weedwacker.GameServer.Data.Excel;

[Columns(33)]
public class AvatarSkillDepotData
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint? energySkill;
	[ColumnIndex(2)]
	public uint? talentSkill;
	[ColumnIndex(3)][TsvArray(4)]
	public uint?[] skills;
	[ColumnIndex(7)][TsvArray(',')]
	public uint[]? subSkills;
	[ColumnIndex(8)]
	public uint? attackModeSkill;
	[ColumnIndex(9)]
	public uint? leaderTalent;
	[ColumnIndex(10)][TsvArray(3)]
	public string[] extraAbilities;
	[ColumnIndex(13)][TsvArray(6)]
	public uint?[] talents;
	[ColumnIndex(19)]
	public string? talentStarName;
	[ColumnIndex(20)]
	public uint? coreProudSkillGroupId;
	[ColumnIndex(21)]
	public uint? coreProudAvatarPromoteLevel;
	[ColumnIndex(22)][TsvArray(5)]
	public ProudSkillOpenConfig[] inherentProudSkillOpens;
	[ColumnIndex(32)]
	public string? skillDepotAbilityGroup;

	[Columns(2)]
	public class ProudSkillOpenConfig
	{
		[ColumnIndex(0)]
		public uint? proudSkillGroupId;
		[ColumnIndex(1)]
		public uint? needAvatarPromoteLevel;
	}
}
