using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class RogueGadgetRotConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public RogueGadgetDirType dir;
	[ColumnIndex(2)]
	public uint rotation;
}