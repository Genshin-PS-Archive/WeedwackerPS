namespace Weedwacker.GameServer.Data.Excel;

public class RewardPreviewExcelConfig
{
	public uint id;
	public string Desc;
	public IdCountStrConfig[] previewItems;

	public class IdCountStrConfig
	{
		public uint id;
		public string count;
	}
}