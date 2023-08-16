namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(7)]
public class FungusFighterMonsterExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint campID;
	[ColumnIndex(2)]
	public uint captureMonsterID;
	[ColumnIndex(3)]
	public uint battleMonsterID;
	[ColumnIndex(4)]
	public uint? defaultName;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] optionalNameID;
	[ColumnIndex(6)]
	public uint inOfficeConfigID; //??????
}
