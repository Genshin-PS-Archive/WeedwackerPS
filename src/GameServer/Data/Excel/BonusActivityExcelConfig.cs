using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class BonusActivityExcelConfig : WatcherConfig
{
	[ColumnIndex(0)]
	public uint bonusActivityId;
	[ColumnIndex(1)]
	public LogicType condComb;
	[ColumnIndex(2)][TsvArray(2)]
	public SignInCondConfig[] condList;
	[ColumnIndex(8)]
	public uint watcherId;
	[ColumnIndex(9)][TsvArray(2)]
	public IdCountConfig[] rewardItemList;
}