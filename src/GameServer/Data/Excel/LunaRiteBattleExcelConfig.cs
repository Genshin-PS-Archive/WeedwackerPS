using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class LunaRiteBattleExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public LunaRiteRegionType regionType;
	[ColumnIndex(2)]
	public uint consecrateID;
	[ColumnIndex(3)]
	public uint groupBundleId;
	public uint rewardID;
	public string challengeIcon;
	public string monsterInfo;
	public string eliteMonsterInfo;
	public uint rulerTextMapHash;
	public uint recipeSourceTextMapHash;
}