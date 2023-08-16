namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class InvestigationDungeonConfig
{
	[ColumnIndex(0)]
	public uint entranceId;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] dungeonIdList;
}