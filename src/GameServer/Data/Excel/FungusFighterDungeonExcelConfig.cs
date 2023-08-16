namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(16)]
public class FungusFighterDungeonExcelConfig
{
	[ColumnIndex(0)]
	public uint DungeonID;
	[ColumnIndex(1)]
	public uint roomCount;
	[ColumnIndex(2)]
	public uint levelType;
	[ColumnIndex(3)]
	public uint stageNumber;
	[ColumnIndex(4)]
	public uint galleryID;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] optionalMushroomNumber;
	[ColumnIndex(6)]
	public uint backupPit;
	[ColumnIndex(7)]
	public uint fungusGroupId;
	[ColumnIndex(8)][TsvArray(',')]
	public uint[] spawnPointConfigIdList;
	[ColumnIndex(9)]
	public uint? transPointConfigId;
	[ColumnIndex(10)]
	public uint? defenseObjectConfigId;
	[ColumnIndex(11)]
	public uint baseScore;
	[ColumnIndex(12)][TsvArray(2)]
	public uint[] timeParams;
	[ColumnIndex(14)]
	public uint deathParam;
	[ColumnIndex(15)]
	public uint guardianParam;
}
