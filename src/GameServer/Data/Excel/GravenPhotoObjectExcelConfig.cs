namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(2)]
public class GravenPhotoObjectExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint? GroupLinkBundle;
}
