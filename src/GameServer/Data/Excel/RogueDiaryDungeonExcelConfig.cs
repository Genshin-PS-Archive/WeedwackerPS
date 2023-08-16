namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class RogueDiaryDungeonExcelConfig
{
	[ColumnIndex(0)]
	public uint dungeonId;
	[ColumnIndex(1)][TsvArray(',')]
    public uint[] groupList;
	[ColumnIndex(2)][TsvArray(3)]
	public RogueDiaryRoundExcelConfig[] roundList;
}