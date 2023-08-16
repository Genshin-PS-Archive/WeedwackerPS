using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class HomeWorldNPCExcelConfig
{
	[ColumnIndex(0)]
	public uint furnitureID;
	[ColumnIndex(1)]
	public uint avatarID;
	[ColumnIndex(2)]
	public uint npcID;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] talkIDs;
	[ColumnIndex(4)]
	public bool isNPC;
	public string headIcon;
	public string frontIcon;
	public string sideIcon;
	public QualityType quality;
	public uint showNameTextMapHash;
	public uint descTextMapHash;
}