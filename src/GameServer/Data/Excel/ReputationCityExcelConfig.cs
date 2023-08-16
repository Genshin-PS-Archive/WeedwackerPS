using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class ReputationCityExcelConfig
{
	[ColumnIndex(0)]
	public uint cityId;
	[ColumnIndex(1)][TsvArray(',')]
	public uint[] exploreAreaVec;
	[ColumnIndex(2)]
	public uint virtualItemId;
	[ColumnIndex(3)]
	public OpenStateType openState;
	public string bgIconPath;
	public string bgEffectPath;
	public uint explainTitleTextMapHash;
	public uint descTextMapHash;
	public uint rewardItemId;
	public uint rewardItemDescTextMapHash;
	public string rewardItemIcon;
	public string rewardBgIcon;
	public string cityIcon;
}