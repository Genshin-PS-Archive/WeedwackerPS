using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class DragonSpineMissionExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint chapterId;
	[ColumnIndex(2)]
	public uint watcherId;
	[ColumnIndex(3)][TsvArray(2)]
	public DragonSpineMissionFinishConfig[] finishExec;

	[Columns(3)]
	public class DragonSpineMissionFinishConfig
	{
		[ColumnIndex(0)]
		public DragonSpineMissionFinishExecType? type;
		[ColumnIndex(1)][TsvArray(2)]
		public string[] param;
	}
}