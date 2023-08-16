namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(6)]
public class InstableSprayGachaExcelConfig
{
	[ColumnIndex(0)]
	public uint IntraPhase;
	[ColumnIndex(1)][TsvArray(4)]
	public uint[] BuffProbabilityWeights;
	[ColumnIndex(5)]
	public uint PhaseDuration;
}
