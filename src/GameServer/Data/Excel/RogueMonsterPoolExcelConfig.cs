using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class RogueMonsterPoolExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint dungeonId;
	[ColumnIndex(2)]
	public RogueMonsterPoolDifficultyType difficulty;
	[ColumnIndex(3)][TsvArray(',')]
    public uint[] poolIdList;
}