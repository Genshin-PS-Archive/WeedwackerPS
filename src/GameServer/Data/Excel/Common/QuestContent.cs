using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(4)]
public class QuestContent
{
	[ColumnIndex(0)]
	public QuestContentType? type;
	[ColumnIndex(1)][TsvArray(3)]
	public int?[] param;
	public string? param_str;
	public uint? count;
}