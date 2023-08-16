namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class LevelTagGroupsExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)][TsvArray(3)]
	public LevelTagGroup[] levelTagGroupList;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] initialLevelTagIdList;
	[ColumnIndex(5)]
	public uint changeCd;

	[Columns(1)]
	public class LevelTagGroup
	{
		[ColumnIndex(0)][TsvArray(',')]
		public uint[] levelTagIdList;
	}
}