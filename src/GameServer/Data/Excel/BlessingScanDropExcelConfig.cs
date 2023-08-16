namespace Weedwacker.GameServer.Data.Excel;

//not in client
[Columns(4)]
public class BlessingScanDropExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint UPPondId;
	[ColumnIndex(2)]
	public uint photoId;
	[ColumnIndex(3)]
	public uint dropProbability;
}
