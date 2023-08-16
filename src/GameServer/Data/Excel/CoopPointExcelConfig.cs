using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(7)]
public class CoopPointExcelConfig
{
	[ColumnIndex(0)]
	public uint id;
	[ColumnIndex(1)]
	public uint chapterId;
	[ColumnIndex(2)]
	public CoopPointType type;
	[ColumnIndex(5)]
	public uint? acceptQuest;
	[ColumnIndex(4)][TsvArray(',')]
	public uint[] postPointList;
	public uint pointNameTextMapHash;
	public uint pointDecTextMapHash;
	[ColumnIndex(6)][TsvArray(',')]
	public uint[] pointPosId;
	public byte photoMaleHashPre;
	public uint photoMaleHashSuffix;
	public byte photoFemaleHashPre;
	public uint photoFemaleHashSuffix;

	//not in client
	[ColumnIndex(3)]
	[TsvArray(',')]
	public uint[] finishQuest; //not sure
}