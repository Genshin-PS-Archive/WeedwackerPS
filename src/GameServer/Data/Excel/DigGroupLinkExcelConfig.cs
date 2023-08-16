namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class DigGroupLinkExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint stage;
	[ColumnIndex(2)]
	public uint groupLinkBundleId;
	[ColumnIndex(3)]
	public uint? groupLinkBundleId2;
	[ColumnIndex(4)]
	public uint? groupLinkChangeCond;
	public uint areaNameTextMapHash;
	public uint[] watcherID;
	public string background;
}