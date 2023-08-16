using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class PSActivitiesActivityConfig : PSActivitiesBaseConfig
{
	public uint activityNameTextMapHash;
	public uint activityDescTextMapHash;
	public PSActivitiesCategoryType category;
	public bool availableByDefault;
	public bool isRequiredForCompletion;
	public bool hidden;
	public bool isOnlineMultiplay;
	public string largeIcon;
	public string smallIcon;
}