using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(9)]
public class ChessCardEffect
{
	[ColumnIndex(0)]
	public ChessCardTargetType? targetType;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] targetParamList;
	[ColumnIndex(2)]
	public ChessCardEffectType? effectType;
	[ColumnIndex(3)]
	public string effectStrParam;
	[ColumnIndex(4)]
	public int? effectParam1;
	[ColumnIndex(5)]
	public int? effectParam2;
	[ColumnIndex(6)]
	public int? effectParam3;
	[ColumnIndex(7)]
	public uint? beginRound;
	[ColumnIndex(8)]
	public uint? activeRounds;
	public uint effectIndex;
}