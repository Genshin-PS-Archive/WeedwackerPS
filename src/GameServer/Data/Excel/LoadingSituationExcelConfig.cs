using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(1)]
public class LoadingSituationExcelConfig
{
	[ColumnIndex(0)]
	public uint stageID;
	public LoadingTipsSituationType loadingSituationType;
	public uint[] sceneID;
	public uint[] area1_ID;
	public LoadingAreaType areaTerrainType;
	public string picPath;
}