namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(4)]
public class GravenCampExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint GroupLinkID;
	[ColumnIndex(2)]
	public uint ChallengeID;
	[ColumnIndex(3)]
	public uint WatcherID;
}
