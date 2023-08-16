using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class RoguelikeCurseExcelConfig
{
	public uint level;
	public uint groupId;
	public uint curseNameTextMapHash;
	public uint descTextMapHash;
	public RoguelikeEffectExcelConfig effectConfig;
	public bool isDynamicShow;
	public bool isClearAtNextLevel;
	public float[] descParamList;
	public bool[] descParamSuperpositionList;
}