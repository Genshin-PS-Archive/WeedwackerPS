namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class RandTaskLevelConfig
{
    [ColumnIndex(0)]
    public uint ID;
    [ColumnIndex(1)]
    public uint minZoneLevel;
    [ColumnIndex(2)]
    public uint maxZoneLevel;
    [ColumnIndex(3)]
    public uint reviseLevel;
}