namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class ExpeditionActivityPreviewExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint questId;
	public uint prequestId;
	public uint npcCityId;
}