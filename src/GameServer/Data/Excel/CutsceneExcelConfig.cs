namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class CutsceneExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string path;
}