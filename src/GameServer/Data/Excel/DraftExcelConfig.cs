using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(22)]
public class DraftExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(4)]
	public DraftTransferConfig[] transferConfig;
	[ColumnIndex(9)]
	public uint? sceneId;
	[ColumnIndex(10)]
	public DraftExecType exec;
	[ColumnIndex(11)]
	public DraftExecSubType? execSubType;
	[ColumnIndex(12)]
	public uint? param;
	[ColumnIndex(13)]
	public bool enableMp;
	[ColumnIndex(14)]
	public bool isNeedAllAgree;
	[ColumnIndex(15)]
	public uint confirmCountDown;
	[ColumnIndex(16)]
	public uint minPlayerCount;
	[ColumnIndex(17)]
	public bool isNeedTwiceConfirm;
	[ColumnIndex(18)]
	public uint twiceConfirmCountDown;
	[ColumnIndex(19)]
	public bool isExecWhenCountDownOver;

	[Columns(2)]
	public class DraftTransferConfig
	{
		[ColumnIndex(0)]
		public uint? group_id;
		[ColumnIndex(1)]
		public uint? config_id;
	}

	//not in client
	[ColumnIndex(20)]
	public uint? invitationType;
	[ColumnIndex(21)]
	public uint? hostOpenRestrictionType;
}