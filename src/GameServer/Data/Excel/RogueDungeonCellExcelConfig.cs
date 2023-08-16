using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class RogueDungeonCellExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint dungeonId;
	[ColumnIndex(2)]
	public uint cellId;
	[ColumnIndex(3)][TsvArray(',')]
    public float[] cellCenterPos;
	[ColumnIndex(4)][TsvArray(',')]
    public uint[] mapCoordinate;
	[ColumnIndex(5)][TsvArray(',')]
    public uint[] adjacencyCellList;
	[ColumnIndex(6)]
	public uint groupId;
	[ColumnIndex(7)]
	public uint weightId;
	[ColumnIndex(8)]
	public float operatorDeltaY;
	[ColumnIndex(9)]
	public float doorOffset;
	[ColumnIndex(10)]
	public RogueCellType specialType;
	[ColumnIndex(11)]
	public float doorDeltaY;
	[ColumnIndex(12)]
	public bool isInitCell;
}