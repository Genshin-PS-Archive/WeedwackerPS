namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class ForgeUpdateExcelConfig
{
	[ColumnIndex(0)]
	public uint playerLevel;
	[ColumnIndex(1)]
	public uint forgeQueueNum;
}