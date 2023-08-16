using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class MechanicBuildingExcelConfig
{
	public uint id;
	public uint gadget_id;
	public uint specialEffectLevel1;
	public uint specialEffectLevel2;
	public uint specialEffectID1;
	public uint specialEffectID2;
	public uint specialEffectDesc1TextMapHash;
	public uint specialEffectDesc2TextMapHash;
	public uint maxLevel;
	public SGVConfig[] openConds;
	public uint buildLimit;
	public bool isEnableRotate;
	public uint[] defaultDungeonList;
	public uint elementType;
}