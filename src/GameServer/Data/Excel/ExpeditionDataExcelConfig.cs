using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(19)]
public class ExpeditionDataExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	public uint nameTextMapHash;
	[ColumnIndex(1)]
	public uint city_id;
	[ColumnIndex(2)][TsvArray(3)]
	public ExpeditionOpenCondition[] open_condition_vec;
	[ColumnIndex(11)][TsvArray(4)]
	public ExpeditionReward[] time_reward_vec;
	public uint descriptionTextMapHash;
	public string picture;
	public float posX;
	public float posY;

	[Columns(3)]
	public class ExpeditionOpenCondition
	{
		[ColumnIndex(0)]
		public ExpeditionOpenCondType? type;
		[ColumnIndex(1)]
		public uint? para;
		[ColumnIndex(2)]
		public uint? para2;
	}
	[Columns(2)]
	public class ExpeditionReward
	{
		[ColumnIndex(0)]
		public uint htime;
		[ColumnIndex(1)]
		public uint reward_drop_id;
		public uint rewardPreview;
	}
}