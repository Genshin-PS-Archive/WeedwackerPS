namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class RogueDiaryRoundExcelConfig
{
	[ColumnIndex(0)]
	public uint RoomCount;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] roundRoomConfigList;
	[ColumnIndex(2)]
	public uint hpCorrection;
}