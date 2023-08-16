namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class LandSoundExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string name;
	[ColumnIndex(2)]
	public string audioName;
}