using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class ReunionPrivilegeExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
    [ColumnIndex(1)]
    public uint dailyNum;
    [ColumnIndex(2)]
    public uint totalNum;
	[ColumnIndex(3)][TsvArray(2)]
    public ReunionPrivilegeConfig[] privilegeType;

	[Columns(2)]
	public class ReunionPrivilegeConfig
	{
		[ColumnIndex(0)]
		public ReunionPrivilegeType type;
		[ColumnIndex(1)]
		public string sub_type;
	}
}