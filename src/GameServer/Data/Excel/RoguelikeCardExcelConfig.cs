using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class RoguelikeCardExcelConfig
{
	public uint id;
	public uint sortOrder;
	public RoguelikeCardLabel label;
	public RoguelikeCardType type;
	public uint[] relatedRuneList;
	public string[] relatedElementList;
	public RoguelikeEffectExcelConfig effectConfig;
	public bool isClearAtNextLevel;
	public bool isDynamicShow;
	public uint cardNameTextMapHash;
	public uint descTextMapHash;
	public uint extraDescTextMapHash;
	public float[] descParamList;
	public bool[] descParamSuperpositionList;
	public float[] descParamBaseValueList;
}