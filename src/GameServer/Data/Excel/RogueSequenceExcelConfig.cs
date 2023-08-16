using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class RogueSequenceExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint dungeonId;
	[ColumnIndex(2)]
	public uint sequenceId;
	[ColumnIndex(3)][TsvArray(',')]
    public uint[] cellList;
	[ColumnIndex(4)][TsvDictionary(';', ',')]
    public Dictionary<uint, uint> cellPriority;
	[ColumnIndex(5)][TsvArray(3)]
	public RogueSequenceCellConfig[] cellSeqList;

	[Columns(2)]
	public class RogueSequenceCellConfig
	{
		[ColumnIndex(0)]
		public RogueCellType type;
		[ColumnIndex(1)][TsvArray(',')]
        public uint[] range;
	}
}