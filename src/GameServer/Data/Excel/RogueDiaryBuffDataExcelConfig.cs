using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(4)]
public class RogueDiaryBuffDataExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	public uint nameTextMapHash;
	public uint descTextMapHash;
	public string[] descParam;
	public uint quality;
	public string icon;
	[ColumnIndex(1)]
	public RogueDiaryBuffType type;
	[ColumnIndex(2)]
	public RogueDiaryBuffEffectType effectType;

	// not in client
	[ColumnIndex(3)]
	public string abilityName;
}