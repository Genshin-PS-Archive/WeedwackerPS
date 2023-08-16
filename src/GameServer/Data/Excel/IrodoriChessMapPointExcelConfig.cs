namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class IrodoriChessMapPointExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	public float coordX;
	public float coordY;

	//not in client
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] spawnerResourcePacks;
}