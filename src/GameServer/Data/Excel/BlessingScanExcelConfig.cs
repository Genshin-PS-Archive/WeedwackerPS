using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class BlessingScanExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint typeId;
	[ColumnIndex(2)]
	public BlessingScanType scanType;
	[ColumnIndex(3)]
	public uint refId;
	[ColumnIndex(4)][TsvArray(2)]
	public BlessingPicUpConfig[] picUpConfig;
	[ColumnIndex(8)]
	public uint? scanDuration;
	[ColumnIndex(9)]
	public uint scanTime;
	public string hitBoxesNodeName;

	[Columns(2)]
	public class BlessingPicUpConfig
	{
		[ColumnIndex(0)]
		public uint? id;
		[ColumnIndex(1)]
		public uint? prob;
	}
}