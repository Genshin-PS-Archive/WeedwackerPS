namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class BirthdayMailExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint mailId;
	[ColumnIndex(2)]
	public uint rewardId;
	[ColumnIndex(3)]
	public string effectiveDate;
	[ColumnIndex(4)]
	public uint effectiveTimestamp;
}