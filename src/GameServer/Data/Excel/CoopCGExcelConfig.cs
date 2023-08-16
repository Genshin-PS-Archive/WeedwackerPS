using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class CoopCGExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint chapterId;
	[ColumnIndex(2)]
	public uint unlockPointId;
	[ColumnIndex(3)]
	public CoopCGType cgType;
	[ColumnIndex(4)][TsvArray(2)]
	public CoopCondConfig[] unlockCond;
	public byte showImageHashPre;
	public uint showImageHashSuffix;
	public byte showImageSmallHashPre;
	public uint showImageSmallHashSuffix;
	public uint cgNameTextMapHash;
	public uint cgDesTextMapHash;
	public uint sortId;
}