using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(18)]
public class AvatarTalentData
{
	[ColumnIndex(0)]
	public uint talentId;
	public uint nameTextMapHash;
	public uint descTextMapHash;
	public string icon;
	[ColumnIndex(13)]
	public uint? prevTalent;
	[ColumnIndex(14)]
	public uint mainCostItemId;
	[ColumnIndex(15)]
	public uint mainCostItemCount;
	[ColumnIndex(16)]
	public uint? viceCostItemId;
	[ColumnIndex(17)]
	public uint? viceCostItemCount;
	[ColumnIndex(1)]
	public string openConfig;
	[ColumnIndex(2)][TsvArray(2)]
	public PropValConfig[] addProps;
	[ColumnIndex(6)][TsvArray(7)]
	public float?[] paramList;
}
