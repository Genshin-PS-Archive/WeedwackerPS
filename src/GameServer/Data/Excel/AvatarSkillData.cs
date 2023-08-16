using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(28)]
public class AvatarSkillData
{
	[ColumnIndex(0)]
	public uint id;
	public uint nameTextMapHash;
	[ColumnIndex(1)]
	public string abilityName;
	public uint descTextMapHash;
	public string skillIcon;
	[ColumnIndex(2)]
	public bool isRanged;
	[ColumnIndex(3)]
	public float cdTime;
	[ColumnIndex(4)]
	public bool ignoreCDMinusRatio;
	[ColumnIndex(5)]
	public float? costStamina;
	[ColumnIndex(6)]
	public ElementType? costElemType;
	[ColumnIndex(7)]
	public float? costElemVal;
	[ColumnIndex(8)]
	public int maxChargeNum;
	[ColumnIndex(9)]
	public int triggerID;
	[ColumnIndex(10)]
	public string lockShape;
	[ColumnIndex(11)][TsvArray(4)]
	public float[] lockWeightParams;
	[ColumnIndex(15)]
	public bool isAttackCameraLock;
	[ColumnIndex(16)]
	public SkillDrag? dragType;
	[ColumnIndex(17)]
	public bool showIconArrow;
	[ColumnIndex(18)]
	public MonitorType? needMonitor;
	[ColumnIndex(19)]
	public bool defaultLocked;
	[ColumnIndex(20)]
	public string buffIcon;
	[ColumnIndex(21)]
	public uint? proudSkillGroupId;
	[ColumnIndex(22)]
	public string globalValueKey; // when this is not empty, set the avatar's global's value to energyMin
	[ColumnIndex(23)]
	public float? energyMin;
	[ColumnIndex(24)]
	public bool forceCanDoSkill;
	[ColumnIndex(25)]
	public uint? cdSlot;
	[ColumnIndex(26)]
	public bool needStore;
	[ColumnIndex(27)]
	public uint? shareCDID;
}
