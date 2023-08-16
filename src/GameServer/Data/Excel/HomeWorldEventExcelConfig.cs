using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class HomeWorldEventExcelConfig
{
	[ColumnIndex(0)]
	public uint eventID;
	[ColumnIndex(1)]
	public HomeAvatarEventType eventType;
	[ColumnIndex(2)]
	public uint avatarID;
	[ColumnIndex(3)]
	public uint? talkID;
	[ColumnIndex(4)]
	public uint? rewardID;
	[ColumnIndex(5)]
	public uint furnitureSuitID;
	[ColumnIndex(6)]
	public uint? lasttime;
	[ColumnIndex(7)]
	public uint? order;
	[ColumnIndex(8)]
	public HomeAvatarEventCondType? conditionType1;
	[ColumnIndex(9)]
	public uint? conditionParam1;
	[ColumnIndex(10)]
	public HomeAvatarEventCondType? conditionType2;
	[ColumnIndex(11)]
	public uint? conditionParam2;
}