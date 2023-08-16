using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(31)]
public class ActivityChessCardExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint number;
	[ColumnIndex(2)]
	public ChessCardTag? tag;
	[ColumnIndex(3)]
	public uint needChessLevel;
	public uint cardNameTextMapHash;
	public uint descTextMapHash;
	public uint extraDescTextMapHash;
	public uint extraDesc2TextMapHash;
	public float[][] descParamList;
	public bool[] descParamSuperpositionList;
	[ColumnIndex(4)]
	public bool isDisused;
	public ChessCardNumericalModificationType cardNumericalModificationType;
	public ChessCardNumericalModificationMode cardNumericalModificationMode;
	public float cardNumericalModificationValue;
	[ColumnIndex(5)]
	public uint costPoints;
	[ColumnIndex(6)]
	public ChessCardType cardType;
	[ColumnIndex(7)]
	public ChessCardQualityType cardQualityType;
	[ColumnIndex(8)]
	public uint cardCount;
	[ColumnIndex(9)]
	public ChessCardEffect effect;
	[ColumnIndex(18)]
	public uint curseWeight;
	[ColumnIndex(19)]
	public bool isCanAttachCurse;
	[ColumnIndex(20)]
	public bool isRemoveOnPick;
	[ColumnIndex(21)]
	public bool isShowOnPick;
	[ColumnIndex(22)][TsvArray(1)]
	public ChessCardEffect[] extraEffectList;
}