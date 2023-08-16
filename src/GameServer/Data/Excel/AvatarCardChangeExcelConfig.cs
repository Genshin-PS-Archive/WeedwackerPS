namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(7)]
public class AvatarCardChangeExcelConfig
{
	[ColumnIndex(0)]
	public uint AvatarID;
	[ColumnIndex(1)][TsvArray(3)]
	public Repeat[] Repeats;

	[Columns(2)]
	public class Repeat
	{
		[ColumnIndex(0)][TsvArray(',')]
		public uint[] repeatNum;
		[ColumnIndex(1)][TsvDictionary(':', ';')]
		public Dictionary<uint, uint> convertedItems;
	}
}
