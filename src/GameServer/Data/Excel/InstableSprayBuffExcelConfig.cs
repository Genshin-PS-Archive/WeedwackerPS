namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(3)]
public class InstableSprayBuffExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string AbilityGroupName;
	[ColumnIndex(2)]
	public string SGV;
}
