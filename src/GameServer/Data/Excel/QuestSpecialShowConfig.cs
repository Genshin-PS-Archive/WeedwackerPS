using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class QuestSpecialShowConfig
{
    [ColumnIndex(0)]
    public uint id;
    [ColumnIndex(1)]
    public QuestSpecialShowType condType;
    [ColumnIndex(2)]
    public uint param1;
    [ColumnIndex(3)]
    public uint param2;
    public uint showTipsTextMapHash;
}