using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(23)]
public class TeamResonanceData
{
	[ColumnIndex(0)]
	public uint teamResonanceId;
	[ColumnIndex(12)]
	public uint teamResonanceGroupId;
	[ColumnIndex(13)]
	public uint level;
	[ColumnIndex(14)]
	public uint? fireAvatarCount;
	[ColumnIndex(15)]
	public uint? waterAvatarCount;
	[ColumnIndex(16)]
	public uint? grassAvatarCount;
	[ColumnIndex(17)]
	public uint? electricAvatarCount;
	[ColumnIndex(18)]
	public uint? iceAvatarCount;
	[ColumnIndex(19)]
	public uint? windAvatarCount;
	[ColumnIndex(20)]
	public uint? rockAvatarCount;
	[ColumnIndex(21)]
	public TeamResonanceCondType? cond;
	[ColumnIndex(22)]
	public uint minTotalPromoteLevel;
	public uint nameTextMapHash;
	public uint descTextMapHash;
	[ColumnIndex(1)]
	public string openConfig;
	public PropValConfig[] addProps;
	[ColumnIndex(2)][TsvArray(10)]
	public float?[] paramList;
}
