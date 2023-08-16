using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(9)]
public class ActivityChessLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint level;
	public uint descTextMapHash;
	[ColumnIndex(1)]
	public uint expToNext;
	[ColumnIndex(2)]
	public uint homeHP;
	[ColumnIndex(3)]
	public uint initialBuild;
	[ColumnIndex(4)]
	public uint cardCount;
	[ColumnIndex(5)]
	public uint cardCost;
	[ColumnIndex(6)]
	public CardFortuneType cardFortune;
	[ColumnIndex(7)][TsvArray(';')]
	public uint[] fortuneList;
	[ColumnIndex(8)]
	public uint freeCardCount;
	public bool isNewGearUnlocked;
	public bool isNewCardUnlocked;
}