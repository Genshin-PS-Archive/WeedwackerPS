using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

public class OfferingOpenStateConfig
{
	public uint offeringId;
	public OpenStateType openState;
	public ItemLimitType itemLimit;
	public bool isContinuousLevelUp;
	public OfferingMaxLevelLimitType maxLevelLimitType;
	public uint activityId;
	public bool isAllowHostInMpMode;
}