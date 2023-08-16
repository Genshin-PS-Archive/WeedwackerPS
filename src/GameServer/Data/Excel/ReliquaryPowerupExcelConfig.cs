namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class ReliquaryPowerupExcelConfig
{
	[ColumnIndex(0)]
	public uint powerupMultiple;

	//not in client
	[ColumnIndex(1)]
	public uint powerupWeighting;
}