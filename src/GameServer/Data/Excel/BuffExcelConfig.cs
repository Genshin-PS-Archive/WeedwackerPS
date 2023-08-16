using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class BuffExcelConfig : BaseServerBuffConfig
{
	[ColumnIndex(0)]
	public uint groupId;
	public string name;
	public string desc;
	[ColumnIndex(1)]
	public float? time;
	[ColumnIndex(2)]
	public BuffStackType? stackType;
	[ColumnIndex(3)]
	public bool? isPersistent;
	[ColumnIndex(4)]
	public bool isDelWhenLeaveScene;
}