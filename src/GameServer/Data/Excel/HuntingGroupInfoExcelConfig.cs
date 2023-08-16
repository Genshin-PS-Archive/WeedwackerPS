using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class HuntingGroupInfoExcelConfig
{
	[ColumnIndex(0)]
	public uint groupId;
	[ColumnIndex(1)]
	public uint regionId;
	[ColumnIndex(2)]
	public HuntingCluePointType pointType;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[]? refIndex;
	[ColumnIndex(4)]
	public HuntingMonsterCreatePosType? posType;
}