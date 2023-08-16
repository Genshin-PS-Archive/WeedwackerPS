using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class LunaRiteBattleBuffExcelConfig
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public LunaRiteRegionType regionType;
	[ColumnIndex(2)]
	public uint number;
	public uint buffTextMapHash;
	[ColumnIndex(3)]
	public uint rewardId;
}