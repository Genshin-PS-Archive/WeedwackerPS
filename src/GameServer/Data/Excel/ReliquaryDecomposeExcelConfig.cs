namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class ReliquaryDecomposeExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(2)]
	public uint needReliquaryCount;
	[ColumnIndex(3)]
	public uint needReliquaryRankLevel;
	public uint rewardPreviewId;
	public uint maxReliquaryNum;
	public uint effectDescTextMapHash;

	//not in client
	[ColumnIndex(1)]
	public uint dropId;
}