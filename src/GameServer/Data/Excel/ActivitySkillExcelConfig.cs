using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(10)]
public class ActivitySkillExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public ActivitySkillTarget skillTarget;
	[ColumnIndex(2)]
	public string abilityName;
	[ColumnIndex(3)]
	public string globalValueKey;
	[ColumnIndex(4)]
	public uint energyMin;
	[ColumnIndex(5)]
	public uint energyMax;
	[ColumnIndex(6)]
	public float cdTime;
	[ColumnIndex(7)]
	public float guideTime;
	public string skillIcon;
	[ColumnIndex(8)]
	[TsvArray(',')]
	public string[] guideKey;
	[ColumnIndex(9)]
	public OpenStateType? guideOpenState;
	public uint unableTextTextMapHash;
	public uint channelTextTextMapHash;
	public uint interruptTextTextMapHash;
}