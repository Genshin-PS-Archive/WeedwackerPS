using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class ReunionWatcherExcelConfig : WatcherConfig
{
	public uint watcherGroupId;
	public uint descTextMapHash;
    public string activateLevelRange;
    public uint rewardId;
    public uint score;
    public uint rewardUnlockDay;
}