using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigAudioDialog
{
	public AudioDialogReaction[] reactions;

	public class AudioDialogReaction
	{
		public uint dialogId;
		public AudioStateOp[] stateOpsOnStart;
		public AudioStateOp[] stateOpsOnFinish;
		public ConfigWwiseString eventOnStart;
		public ConfigWwiseString eventOnFinish;
	}
}