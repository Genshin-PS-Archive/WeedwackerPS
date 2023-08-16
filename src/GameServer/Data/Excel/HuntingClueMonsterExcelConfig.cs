using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class HuntingClueMonsterExcelConfig
{
	[ColumnIndex(0)]
	public uint configId;
	[ColumnIndex(1)]
	public uint monsterId;
	[ColumnIndex(2)]
	public uint reviseLevelId;
	[ColumnIndex(3)]
	public HuntingMonsterGroupType groupType;
	[ColumnIndex(4)]
	public uint monsterGroupId;
	[ColumnIndex(5)]
	public uint level;
	[ColumnIndex(6)]
	public bool isClueMonster;
}