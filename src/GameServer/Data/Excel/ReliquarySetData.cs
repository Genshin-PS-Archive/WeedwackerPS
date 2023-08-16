namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class ReliquarySetData
{
	[ColumnIndex(0)]
	public uint setId;
	public string setIcon;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] setNeedNum;
	[ColumnIndex(2)]
	public uint? EquipAffixId;
	[ColumnIndex(3)]
	public uint? DisableFilter;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] containsList;
	[ColumnIndex(5)]
	public uint? bagSortValue;

	//not in client
	[ColumnIndex(6)]
	public uint? warrantyId;
}
