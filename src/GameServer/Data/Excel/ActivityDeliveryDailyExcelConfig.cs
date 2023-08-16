namespace Weedwacker.GameServer.Data.Excel;

[Columns(20)]
public class ActivityDeliveryDailyExcelConfig
{
	[ColumnIndex(0)]
	public uint dailyConfigId;
	[ColumnIndex(1)][TsvArray(3)]
	public DeliveryQuestConfig[] deliveryQuestConfig;
	public uint[] taskDesc;
	[ColumnIndex(19)]
	public uint dailyRewardId;

	[Columns(6)]
	public class DeliveryQuestConfig
	{
		[ColumnIndex(0)]
		public uint parentQuestId;
		[ColumnIndex(1)]
		public uint deliveryQuestId;
		[ColumnIndex(2)]
		public uint startQuestId;
		[ColumnIndex(3)]
		public uint talkQuestId;
		[ColumnIndex(4)][TsvArray(',')]
		public uint[] watcherId;
		[ColumnIndex(5)]
		public uint itemId;
	}
}