using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class RegionSearchCondExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint searchNameTextMapHash;
	public uint searchDescTextMapHash;
	public uint searchMapDescTextMapHash;
	[ColumnIndex(1)]
	public uint groupId;
	[ColumnIndex(2)]
	public LogicType logicType;
	[ColumnIndex(3)][TsvArray(2)]
	public RegionSearchCond[] cond;
	[ColumnIndex(9)][TsvArray(',')]
	public uint[] regionList;
	[ColumnIndex(10)]
	public uint rewardId;
	[ColumnIndex(11)]
	public uint totalProgress;
	public uint reminderId;

	[Columns(3)]
	public class RegionSearchCond
	{
		[ColumnIndex(0)]
		public RegionSearchCondType? type;
		[ColumnIndex(1)][TsvArray(2)]
		public int?[] param;
	}
}