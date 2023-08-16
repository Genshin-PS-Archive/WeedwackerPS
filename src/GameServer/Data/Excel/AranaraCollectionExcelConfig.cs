namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(2)]
public class AranaraCollectionExcelConfig
{
	[ColumnIndex(0)]
	public uint CollectionID;
	[ColumnIndex(1)]
	public uint Type; //enum
}
