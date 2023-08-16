using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class ReputationEntranceExcelConfig
{
	[ColumnIndex(0)]
	public uint textId; // not sure why it's called textId, in gio resources it's just called ID
	public ReputationEntranceType entranceId;
	[ColumnIndex(1)]
	public uint cityId;
	[ColumnIndex(2)]
	public LogicType condComb;
	[ColumnIndex(3)][TsvArray(2)]
    public ReputationEntranceCond[] condVec;
	public uint[] condNameVec;
	public uint nameTextMapHash;
	public uint titleTextMapHash;
	public uint explainTitleTextMapHash;
	public uint descTextMapHash;
	public string iconName;
	public uint order;

	[Columns(3)]
	public class ReputationEntranceCond
	{
		[ColumnIndex(0)]
		public ReputationEntranceCondType type;
        [ColumnIndex(1)]
        public uint param1;
        [ColumnIndex(2)]
        public uint param2;
	}
}