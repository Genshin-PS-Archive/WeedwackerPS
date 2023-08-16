using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class RogueTokenExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint stageId;
	[ColumnIndex(2)]
	public uint level;
	[ColumnIndex(3)]
	public RogueMonsterPoolDifficultyType cellType;
	[ColumnIndex(4)][TsvArray(',')]
    public uint[] coinANum;
	[ColumnIndex(5)][TsvArray(',')]
    public uint[] coinBNum;
	[ColumnIndex(6)][TsvArray(',')]
    public uint[] coinCNum;
}