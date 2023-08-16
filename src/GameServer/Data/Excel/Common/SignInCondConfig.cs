using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(3)]
public class SignInCondConfig
{
	[ColumnIndex(0)]
	public SignInCondType? type;
	[ColumnIndex(1)][TsvArray(2)]
	public int?[] paramList;
}