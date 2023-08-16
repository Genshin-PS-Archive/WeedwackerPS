namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class QuestGlobalVarConfig
{
    [ColumnIndex(0)]
    public uint id;
    [ColumnIndex(1)]
    public int defaultValue;
}