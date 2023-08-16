namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class AttackAttenuationExcelConfig
{
	[ColumnIndex(0)]
	public string group;
	[ColumnIndex(1)]
	public float resetCycle;
	[ColumnIndex(2)][TsvArray(',')]
	public float?[] durabilitySequence;
	[ColumnIndex(3)][TsvArray(',')]
	public float?[] enbreakSequence;
	[ColumnIndex(4)][TsvArray(',')]
	public float[] damageSequence;
}