using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class BartenderFormulaExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)][TsvArray(2)]
	public BartenderMaterial[] BaseMaterial;
	[ColumnIndex(5)][TsvArray(2)]
	public BartenderMaterial[] ExtraMaterial;
	[ColumnIndex(9)][TsvArray(',')]
	public uint[] AvailableAffixList;
	public BartenderMixingState mixingState;
	public uint descTextMapHash;
	public uint nameTextMapHash;
	public uint type;
	public uint lockType;
	public uint blurredDescTextMapHash;

	//not in client
	[ColumnIndex(10)]
	public bool unlockByDefault;
	[ColumnIndex(11)]
	public uint? questId;
}