using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class RqTalkExcelConfig
{
    public uint id;
    public TalkBeginWay beginWay;
    public PlayMode activeMode;
    public LogicType beginCondComb;
    public TalkCond[] beginCond;
    public uint priority;
    public uint[] nextTalks;
    public uint[] nextRandomTalks;
    public int showRandomTalkCount;
    public uint initDialog;
    public uint[] npcId;
    public string performCfg;
    public TalkHeroType heroTalk;
    public uint questId;
    public bool dontBlockDaily;
}
