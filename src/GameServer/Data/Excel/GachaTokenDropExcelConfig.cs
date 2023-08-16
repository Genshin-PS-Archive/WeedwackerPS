namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(2)]
public class GachaTokenDropExcelConfig
{
	[ColumnIndex(0)]
	public uint ItemSubType;
	[ColumnIndex(1)]
	public uint TokenDropID;
}
