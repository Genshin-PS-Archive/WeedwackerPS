namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class ExpeditionActivityMarkerExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public float posX;
	public float posY;
	public byte pictureHashPre;
	public uint pictureHashSuffix;
}