namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class HomeWorldComfortLevelExcelConfig
{
	[ColumnIndex(0)]
	public uint levelID;
	[ColumnIndex(1)]
	public uint comfort;
	[ColumnIndex(2)]
	public uint homeCoinProduceRate;
	[ColumnIndex(3)]
	public uint companionshipExpProduceRate;
	[ColumnIndex(4)]
	public string levelNameText;
	//public uint levelNameTextMapHash;
	public byte levelIconHashPre;
	[ColumnIndex(5)]
	public string levelIcon;
	//public uint levelIconHashSuffix;
}