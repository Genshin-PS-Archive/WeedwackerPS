using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class CaptureTagsExcelConfig
{
	[ColumnIndex(0)]
	public uint captureTagID;
	[ColumnIndex(1)]
	public CaptureCodexShowType codexShowType;
	[ColumnIndex(2)]
	public ItemLimitType itemLimitType;
}