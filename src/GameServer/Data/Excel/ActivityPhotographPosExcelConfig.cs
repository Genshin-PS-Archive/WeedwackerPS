namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class ActivityPhotographPosExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint groupLinkId;
	public uint posTitleTextMapHash;
	public uint requireTitleInActivityPageTextMapHash;
	public uint requireDescInActivityPageTextMapHash;
	public uint npcInviteDescTextMapHash;
	public uint npcCommentDescTextMapHash;
	[ColumnIndex(2)]
	public string picSmall;
	[ColumnIndex(3)]
	public string picBig;
	public uint photoCheckRootID;
	public uint rootNodeDescTextMapHash;
	public uint[] photoCheckSubNodeID;
	public uint[] photoCheckSubNodeDesc;
	[ColumnIndex(4)]
	public uint openDay;
	[ColumnIndex(5)]
	public uint watcherId;
	[ColumnIndex(6)]
	public uint galleryId;
	[ColumnIndex(7)]
	public uint redPointID;

	//not in client
	[ColumnIndex(8)][TsvArray(',')]
	public string[]? DefaultCameraPreviewScreen;
	[ColumnIndex(9)][TsvArray(',')]
	public string[] DefaultCameraPreviewEmpty;
	[ColumnIndex(10)]
	public uint SceneID;
}