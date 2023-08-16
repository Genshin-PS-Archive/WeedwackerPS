using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class ReliquaryData : ItemConfig
{
	[ColumnIndex(0)]
	public EquipType equipType;
	public string showPic;
	[ColumnIndex(1)]
	public uint rankLevel;
	[ColumnIndex(2)]
	public uint mainPropDepotId;
	[ColumnIndex(3)]
	public uint appendPropDepotId;
	[ColumnIndex(4)]
	public uint appendPropNum;
	[ColumnIndex(5)]
	public uint? setId;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] addPropLevels;
	[ColumnIndex(7)]
	public uint baseConvExp;
	[ColumnIndex(8)]
	public uint maxLevel;
	public uint storyId;
	[ColumnIndex(9)]
	public MaterialDestroyType? destroyRule;
	[ColumnIndex(10)][TsvArray(',')]
	public uint[] destroyReturnMaterial;
	[ColumnIndex(11)][TsvArray(',')]
	public uint[] destroyReturnMaterialCount;
	[ColumnIndex(12)]
	public uint initialLockState;
}
