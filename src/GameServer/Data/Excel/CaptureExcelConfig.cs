namespace Weedwacker.GameServer.Data.Excel;

[Columns(16)]
public class CaptureExcelConfig
{
	[ColumnIndex(0)]
	public uint monsterID;
	[ColumnIndex(1)][TsvArray(5)]
	public CaptureTagConfig[] captureTagConfig;

	[Columns(3)]
	public class CaptureTagConfig
	{
		[ColumnIndex(0)]
		public uint? captureType;
		[ColumnIndex(1)]
		public uint? dropID;
		[ColumnIndex(2)]
		public uint? itemID;
	}
}