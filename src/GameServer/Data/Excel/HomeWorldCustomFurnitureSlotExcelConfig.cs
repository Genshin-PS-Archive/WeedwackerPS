
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class HomeWorldCustomFurnitureSlotExcelConfig : CustomSlotConfig
{
	public uint slotNameTextMapHash;
	public uint noPartsTipsTextMapHash;
	public uint getPartsTipsTextMapHash;
	public uint[] rootGadgetIdList;
	public string[] slotIdentifierPathList;
	public uint[] dependentSlotIdList;
	public string pageTitle;
	public string effectSlotSelect;
	public string effectPartsSetup;
}