namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class GadgetChainExcelConfig
{
	[ColumnIndex(0)]
	public uint chainId;
	[ColumnIndex(1)]
	public uint initLevel;
	[ColumnIndex(2)]
	public uint maxLevel;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] buffList;

	//not in client
	[ColumnIndex(4)]
	public bool allowModificationByLua;
}