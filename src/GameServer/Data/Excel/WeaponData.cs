using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(22)]
public class WeaponData : ItemConfig
{
	[ColumnIndex(0)]
	public WeaponType weaponType;
	[ColumnIndex(1)]
	public uint rankLevel;
	[ColumnIndex(2)]
	public WeaponMaterialType? materialType;
	[ColumnIndex(3)]
	public ElementType? elemType;
	[ColumnIndex(4)]
	public bool isGold;
	[ColumnIndex(5)]
	public uint weaponBaseExp;
	[ColumnIndex(6)][TsvArray(2)]
	public uint?[] skillAffix;
	[ColumnIndex(8)]
	public uint? awakenMaterial;
	[ColumnIndex(9)][TsvArray(2)]
	public WeaponProperty[] weaponProp;
	public string awakenTexture;
	public string awakenLightMapTexture;
	public string awakenIcon;
	public bool unRotate;
	[ColumnIndex(15)]
	public uint weaponPromoteId;
	public uint storyId;
	[ColumnIndex(16)][TsvArray(',')]
	public uint[] awakenCosts;
	public byte gachaCardNameHashPre;
	public uint gachaCardNameHashSuffix;
	[ColumnIndex(17)]
	public uint enhanceRule;
	[ColumnIndex(18)]
	public MaterialDestroyType destroyRule;
	[ColumnIndex(19)][TsvArray(',')]
	public uint[] destroyReturnMaterial;
	[ColumnIndex(20)][TsvArray(',')]
	public uint[] destroyReturnMaterialCount;
	[ColumnIndex(21)]
	public uint initialLockState;

	[Columns(3)]
	public class WeaponProperty
	{
		[ColumnIndex(0)]
		public FightPropType propType;
		[ColumnIndex(1)]
		public float initValue;
		[ColumnIndex(2)]
		public GrowCurveType type;
	}
}
