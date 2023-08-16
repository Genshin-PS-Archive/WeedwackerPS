namespace Weedwacker.GameServer.Data.Excel;

[Columns(2)]
public class ActivityCrystalLinkCondBuffExcelConfig
{
	[ColumnIndex(0)]
	public uint buffId;
	[ColumnIndex(1)]
	public string abilityGroupName;
	public string abilityName;
	public uint AbilityTitleTextMapHash;
	public uint AbilityDescTextMapHash;
	public byte iconNameHashPre;
	public uint iconNameHashSuffix;
	public string[] desParam;
}