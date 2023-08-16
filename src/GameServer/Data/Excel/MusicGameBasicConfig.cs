namespace Weedwacker.GameServer.Data.Excel;

[Columns(6)]
public class MusicGameBasicConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint musicID;
	[ColumnIndex(2)]
	public uint musicLevel;
	public string jsonPath;
	public uint longPressInterval;
	public float longPressDownSpeed;
	public uint longPressPreTime;
	public float successPrePoint;
	public float successLatePoint;
	public uint scaleTime;
	public uint lateDropTime;
	public float scaleRange;
	[ColumnIndex(3)][TsvArray(',')]
	public uint[] scoreLevelList;
	public uint scoreOneKey;
	public uint scoreGreat;
	public uint scoreLongPress;
	public float bpm;
	public Dictionary<uint, float> bpmDict;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] ugcBeatDivisionOptions;
	[ColumnIndex(5)]
	public uint? ugcDefaultBeatDivision;
	public string simpleHintJsonPath;
	public string complexHintJsonPath;
	[ColumnIndex(6)]
	public uint noteCount;
	public ComboConfig[] comboConfig;
	public uint unlockTipsTextMapHash;
	public SoloConfig[] soloConfig;

	public class ComboConfig
	{
		public uint comboUpLimit;
		public float rate;
	}
	public class SoloConfig
	{
		public uint soloStart;
		public uint soloEnd;
		public bool isSelf;
	}
}