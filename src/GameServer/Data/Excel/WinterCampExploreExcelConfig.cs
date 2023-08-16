namespace Weedwacker.GameServer.Data.Excel;

public class WinterCampExploreExcelConfig
{
	public uint id;
	public uint openDay;
	public uint priority;
	public uint groupID;
	public uint groupLinkID;
	public uint rewardID;
	public WinterCampDropGroup[] dropGroups;

	public class WinterCampDropGroup
	{
		public uint[] dropId;
	}
}