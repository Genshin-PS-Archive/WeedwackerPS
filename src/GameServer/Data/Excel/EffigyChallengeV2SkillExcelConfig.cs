namespace Weedwacker.GameServer.Data.Excel;

//not in 2.8 client
[Columns(2)]
public class EffigyChallengeV2SkillExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public string AbilityName;
}
