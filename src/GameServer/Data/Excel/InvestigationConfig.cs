using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class InvestigationConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[]? nextInvestigationIdList;
	[ColumnIndex(3)]
	public OpenStateType? unlockOpenStateType;
	[ColumnIndex(4)]
	public uint? unlockLevel;
	[ColumnIndex(5)]
	public uint rewardId;
	public uint titleTextMapHash;
	[ColumnIndex(6)]
	public InvestigationType investigationType;
}