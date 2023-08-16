namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class FungusFighterDungeonQSTExcelConfig
{
	[ColumnIndex(0)]
	public uint plotID;
	[ColumnIndex(1)]
	public uint dungeonID;
	[ColumnIndex(2)][TsvArray(',')]
	public uint[] optionalMushroomID;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] fixedMushroomID;
}
