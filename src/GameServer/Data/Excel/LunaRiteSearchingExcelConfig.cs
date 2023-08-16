using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class LunaRiteSearchingData
{
	[ColumnIndex(0)]
	public uint Id;
	[ColumnIndex(1)]
	public LunaRiteRegionType regionType;
	public uint openDay;
	public uint progressWatcherID;
	public uint[] watcherID;
	public float[] regionCenter;
	public byte regionNameHashPre;
	public uint regionNameHashSuffix;
	public float regionRadius;
    [ColumnIndex(2)]
    public uint chestCond;
    [ColumnIndex(3)]
    public uint runeCond;
    [ColumnIndex(4)]
    public uint chestMarkNum;
    [ColumnIndex(5)]
    public uint runeMarkNum;
}