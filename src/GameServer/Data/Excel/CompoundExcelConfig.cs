using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(20)]
public class CompoundExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint groupID;
	public uint nameTextMapHash;
	public uint rankLevel;
	[ColumnIndex(2)]
	public CompoundType type;
	[ColumnIndex(3)]
	public bool isDefaultUnlocked;
	[ColumnIndex(4)]
	public uint costTime;
	[ColumnIndex(5)]
	public uint queueSize;
	[ColumnIndex(6)][TsvArray(5)]
	public IdCountConfig[] inputVec;
	[ColumnIndex(16)][TsvArray(2)]
	public IdCountConfig[] outputVec;
	public string icon;
	public uint descTextMapHash;
	public uint countDescTextMapHash;
}