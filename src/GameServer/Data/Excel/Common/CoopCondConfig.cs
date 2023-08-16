using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(3)]
public class CoopCondConfig
{
	[ColumnIndex(0)]
	public CoopTaskCondType? condType;
	[ColumnIndex(1)][TsvArray(2)]
	public uint?[] args;
}