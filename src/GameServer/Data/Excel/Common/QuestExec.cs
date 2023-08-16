using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(4)]
public class QuestExec
{
	[ColumnIndex(0)]
	public QuestExecType? type;
	[ColumnIndex(1)][TsvArray(3)]
	public string[] param;
}
