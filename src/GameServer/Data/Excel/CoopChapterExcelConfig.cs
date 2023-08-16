using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class CoopChapterExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint avatarId;
	public uint chapterNameTextMapHash;
	public uint coopPageTitleTextMapHash;
	public uint chapterSortId;
	public uint avatarSortId;
	public string chapterIcon;
	[ColumnIndex(2)][TsvArray(2)]
	public CoopCondConfig[] unlockCond;
	public uint[] unlockCondTips;
	[ColumnIndex(8)]
	public uint openMaterialId;
	[ColumnIndex(9)]
	public uint openMaterialNum;
	[ColumnIndex(10)]
	public string beginTimeStr;
	public uint confidenceValue;
	public string pointGraphPath;
	public float graphXRatio;
	public float graphYRatio;
}