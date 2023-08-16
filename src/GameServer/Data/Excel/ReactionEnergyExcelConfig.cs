using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(11)]
public class ReactionEnergyExcelConfig
{
	[ColumnIndex(0)]
	public ElementReactionType type;
    [ColumnIndex(1)]
	public float minDurability;
    [ColumnIndex(2)]
    public float maxDurability;
    [ColumnIndex(3)]
    public float costRatio;
    [ColumnIndex(4)]
    public uint configID;
    [ColumnIndex(5)]
    public float poolSize;
    [ColumnIndex(6)]
    public float poolRevivePeriod;
    [ColumnIndex(7)]
    public float poolReviveEnergy;
    [ColumnIndex(8)]
    public bool isPersistent;
    [ColumnIndex(9)]
    public float costPeriod;
    [ColumnIndex(10)]
    public uint dropProb;
}