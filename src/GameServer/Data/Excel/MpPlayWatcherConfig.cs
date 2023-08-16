using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

public class MpPlayWatcherConfig : WatcherConfig
{
	public uint MpPlayId;
	public uint priority;
	public bool isStore;
	public uint challengeDescTextMapHash;
	public uint challengeTitleTextMapHash;
}