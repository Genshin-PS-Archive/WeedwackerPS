namespace Weedwacker.GameServer.Data.Excel;


//not in 2.8 client
[Columns(5)]
public class ActivityJigsawDetailExcelConfig
{
	[ColumnIndex(0)]
	public uint PuzzleID;
	[ColumnIndex(1)]
	public uint GadgetID;
	[ColumnIndex(2)]
	public uint LocationID;
	[ColumnIndex(3)]
	public uint MaterialID;
	[ColumnIndex(4)]
	public string RotationEffect;
}
