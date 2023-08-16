namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(11)]
public class ComponentExcelConfig
{
	[ColumnIndex(0)]
	public uint ComponentID;
	[ColumnIndex(1)]
	public uint TypeID;
	[ColumnIndex(2)]
	public string BrickName;
	[ColumnIndex(3)]
	public uint displayGadgetID;
	[ColumnIndex(4)]
	public uint ServerGadgetID;
	[ColumnIndex(5)]
	public uint GadgetLevel;
	[ColumnIndex(6)]
	public uint? ToRotate;
	[ColumnIndex(7)]
	public uint ComponentCost;
	[ColumnIndex(8)]
	public uint? MaxPlacements;
	[ColumnIndex(9)]
	public bool IsVisible;
	[ColumnIndex(10)]
	public uint? type;
}
