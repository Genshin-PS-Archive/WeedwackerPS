using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class CookBonusExcelConfig
{
	[ColumnIndex(0)]
	public uint avatarId;
	[ColumnIndex(1)]
	public uint recipeId;
	[ColumnIndex(2)]
	public CookBonusType bonusType;
	[ColumnIndex(3)][TsvArray(2)]
	public uint?[] paramVec;
	[ColumnIndex(5)][TsvArray(',')]
	public uint[] complexParamVec;
}