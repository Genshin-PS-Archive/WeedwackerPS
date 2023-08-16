namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class BounceConjuringChapterExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint chapterId;
	[ColumnIndex(2)]
	public uint openDay;
	[ColumnIndex(3)]
	public uint galleryId;
	[ColumnIndex(4)]
	public uint DraftID;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] watcherIdList;
	public uint titleTextMapHash;
	public uint descTextMapHash;
	public uint[] otherBallList;
	public uint[] newBallList;
	public uint[] otherItemList;
	public uint[] upItemList;
	public float[] pos;
}