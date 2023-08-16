namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class HomeworldAnimalExcelConfig
{
	[ColumnIndex(0)]
	public uint furnitureID;
	[ColumnIndex(1)]
	public uint monsterID;
	[ColumnIndex(2)]
	public uint isRebirth;
	[ColumnIndex(3)]
	public uint rebirthCD;
}