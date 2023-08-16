using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

public class WorldExcelConfig
{
	public uint id;
	public WorldType type;
	public uint mainSceneId;
	public uint[] subSceneIdVec;
}