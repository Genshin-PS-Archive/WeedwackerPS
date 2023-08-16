using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class OpenStateData
{
	[ColumnIndex(0)]
	public uint id; // maps to OpenStateType enum
	[ColumnIndex(1)]
	public bool defaultState;
	[ColumnIndex(2)]
	public bool allowClientOpen;
	[ColumnIndex(3)][TsvArray(2)]
	public OpenStateCond[] cond;
	[ColumnIndex(9)]
	public uint? system_open_ui_id;

	[Columns(3)]
	public class OpenStateCond
	{
		[ColumnIndex(0)]
		public OpenStateCondType? cond_type;
		[ColumnIndex(1)]
		public uint? param;
		[ColumnIndex(2)]
		public uint? param2;
	}
}
