using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(17)]
public class IrodoriChessCardExcelConfig
{
	[ColumnIndex(0)]
	public uint ID;
	[ColumnIndex(1)]
	public uint costPoints;
	[ColumnIndex(2)]
	public IrodoriChessCardEffect effect;
	[ColumnIndex(9)][TsvArray(1)]
	public IrodoriChessCardEffect[] extraEffectList;
	[ColumnIndex(16)]
	public IrodoriChessCardType cardType;
	public IrodoriChessCardQualityType cardQualityType;
	public IrodoriChessCardTag tag;
	public uint cardNameTextMapHash;
	public uint descTextMapHash;
	public float[] descParam;
	public IrodoriCardNumerical[] cardNumericalList;

	[Columns(7)]
	public class IrodoriChessCardEffect
	{
		[ColumnIndex(0)]
		public IrodoriChessCardTargetType? targetType;
		[ColumnIndex(1)][TsvArray(',')]
		public uint[] targetParamList;
		[ColumnIndex(2)]
		public IrodoriChessCardEffectType? effectType;
		[ColumnIndex(3)]
		public string effectStrParam;
		[ColumnIndex(4)]
		public int? effectParam1;
		[ColumnIndex(5)]
		public int? effectParam2;
		[ColumnIndex(6)]
		public int? effectParam3;
		public uint effectIndex;
	}
	public class IrodoriCardNumerical
	{
		public IrodoriChessCardNumericalModificationType cardNumericalModificationType;
		public IrodoriChessCardNumericalModificationMode cardNumericalModificationMode;
		public float cardNumericalModificationValue;
	}
}