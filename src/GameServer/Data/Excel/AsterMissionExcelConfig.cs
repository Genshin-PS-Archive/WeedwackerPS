namespace Weedwacker.GameServer.Data.Excel;

[Columns(3)]
public class AsterMissionExcelConfig
{
	[ColumnIndex(0)]
	public uint missionId;
	[ColumnIndex(1)]
	public uint phase;
	[ColumnIndex(2)]
	public uint watcherId;
	public uint transPointId;
	public string tracePoint;
	public byte perfabPathHashPre;
	public uint perfabPathHashSuffix;
}