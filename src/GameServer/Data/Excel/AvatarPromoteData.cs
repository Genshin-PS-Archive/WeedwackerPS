using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(22)]
public class AvatarPromoteData
{
	[ColumnIndex(0)]
	public uint avatarPromoteId;
	[ColumnIndex(1)]
	public uint promoteLevel;
	[ColumnIndex(2)]
	public string promoteAudio;
	[ColumnIndex(3)]
	public uint scoinCost;
	[ColumnIndex(4)][TsvArray(4)]
	public IdCountConfig[] costItems;
	[ColumnIndex(12)]
	public uint unlockMaxLevel;
	[ColumnIndex(13)][TsvArray(4)]
	public PropValConfig[] addProps;
	[ColumnIndex(21)]
	public uint requiredPlayerLevel;
}
