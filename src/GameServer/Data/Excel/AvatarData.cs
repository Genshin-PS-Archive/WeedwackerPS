using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;
[Columns(20)]
public class AvatarData : CreatureExcelConfig
{
	[ColumnIndex(0)]
	public AvatarUseType useType;
	public BodyType bodyType;
	public byte scriptDataPathHashPre;
	public uint scriptDataPathHashSuffix;
	public string iconName;
	public string sideIconName;
	[ColumnIndex(3)]
	public QualityType qualityType;
	[ColumnIndex(4)]
	public float chargeEfficiency;
	[ColumnIndex(5)]
	public float healAdd;
	[ColumnIndex(6)]
	public float healedAdd;
	[ColumnIndex(7)]
	public string combatConfig;
	//public byte combatConfigHashPre;
	//public uint combatConfigHashSuffix;
	[ColumnIndex(8)]
	public bool isRangeAttack;
	[ColumnIndex(9)]
	public uint initialWeapon;
	[ColumnIndex(10)]
	public WeaponType weaponType;
	public byte manekinPathHashPre;
	public uint manekinPathHashSuffix;
	public string imageName;
	public byte gachaCardNameHashPre;
	public uint gachaCardNameHashSuffix;
	public byte gachaImageNameHashPre;
	public uint gachaImageNameHashSuffix;
	public byte coopPicNameHashPre;
	public uint coopPicNameHashSuffix;
	public string cutsceneShow;
	[ColumnIndex(11)]
	public uint skillDepotId;
	[ColumnIndex(12)]
	public float staminaRecoverSpeed;
	[ColumnIndex(13)][TsvArray(',')]
	public uint[]? candSkillDepotIds;
	public byte manekinJsonConfigHashPre;
	public uint manekinJsonConfigHashSuffix;
	public uint manekinMotionConfig;
	public uint descTextMapHash;
	[ColumnIndex(14)]
	public AvatarIdentityType avatarIdentityType;
	[ColumnIndex(15)]
	public uint avatarPromoteId;
	[ColumnIndex(16)][TsvArray(';')]
	public uint[] avatarPromoteRewardLevelList;
	[ColumnIndex(17)][TsvArray(';')]
	public uint[] avatarPromoteRewardIdList;
	[ColumnIndex(18)]
	public uint featureTagGroupID;
	[ColumnIndex(19)]
	public string textNameDesc;
	//public uint infoDescTextMapHash;
	public byte animatorConfigPathHashPre;
	public uint animatorConfigPathHashSuffix;

	//not in client
	[ColumnIndex(1)]
	public float reaction_crit_rate;
	[ColumnIndex(2)]
	public float reaction_crit_damage; //guessed
}
