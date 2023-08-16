namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class HomeworldModuleExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public bool isFree;
	public uint unlockTipIfUnFreeTextMapHash;
	[ColumnIndex(2)]
	public uint worldSceneId;
	[ColumnIndex(3)]
	public uint defaultRoomSceneId;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] optionalRoomSceneIdVec;
	public uint moduleNameTextMapHash;
	public uint moduleDescTextMapHash;
	public string[] region;
	public string[] regionPointPos;
	public string smallImageAddr;
	public string bigImageAddr;
}