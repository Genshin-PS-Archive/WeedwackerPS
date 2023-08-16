namespace Weedwacker.GameServer.Data.Excel.Common;

[Columns(6)]
public class CustomSlotConfig
{
	[ColumnIndex(0)]
	public uint slotID;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] optionalPartsIdList;
	[ColumnIndex(2)]
	public bool isNecessary;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] defaultPartsIdList;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] passPartIdList;
	[ColumnIndex(5)]
	public uint? initPartsId;
}