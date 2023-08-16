namespace Weedwacker.GameServer.Data.Excel;

public class RogueDiaryRoundRoomExcelConfig
{
	public uint id;
	public uint HardRoomCount;
	public RogueDiaryRoomTypeConfig[] roomTypeConfigList;

	public class RogueDiaryRoomTypeConfig
	{
		public uint[] normalRoomConfigList;
		public uint[] hardRoomConfigList;
	}
}