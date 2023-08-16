using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class DisplayItemData : ItemConfig
{
	public uint typeDescTextMapHash;
	[ColumnIndex(0)]
	public uint rankLevel;
	[ColumnIndex(1)]
	public DisplayItemType? displayType;
	[ColumnIndex(2)]
	public uint? param;
}
