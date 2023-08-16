using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class PSActivitiesTaskConfig : PSActivitiesBaseConfig
{
	public uint activityID;
	public uint taskNameTextMapHash;
	public bool isRequiredForCompletion;
	public bool hidden;
}