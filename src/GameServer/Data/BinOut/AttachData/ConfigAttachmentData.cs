using Weedwacker.GameServer.Data.BinOut.Shared;

namespace Weedwacker.GameServer.Data;

public class ConfigAttachmentData
{
	public Dictionary<string, ConfigAttachment> attachMap;

	public class ConfigAttachment
	{
		public string slotName;
		public string attachPath;
		public string effectPath;
		public Vector attachPos;
		public Vector attachRot;
		public Vector attachScale;
	}
}