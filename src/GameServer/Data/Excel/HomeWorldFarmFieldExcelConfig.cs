using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class HomeWorldFarmFieldExcelConfig
{
	public uint fieldItemID;
	public HomeWorldFieldType fieldType;
	[ColumnIndex(0)]
	public uint fieldGadgetID;
	[ColumnIndex(1)]
	public uint fieldSlotNum;
	[ColumnIndex(2)]
	[TsvArray(4)]
	public uint[] fieldSlotGadgetID;
}