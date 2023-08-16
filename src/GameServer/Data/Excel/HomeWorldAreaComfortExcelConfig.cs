using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class HomeWorldAreaComfortExcelConfig
{
	[ColumnIndex(0)]
	public uint configID;
	[ColumnIndex(1)]
	public uint sceneID;
	[ColumnIndex(2)]
	public uint areaID;
	[ColumnIndex(3)]
	public HomeWorldAreaType areaType;
	[ColumnIndex(4)]
	public string nameText;
	//public uint nameTextMapHash;
	[ColumnIndex(5)]
	public uint maxComfort;
	[ColumnIndex(6)]
	public string safePointStr;
	public uint lowLoadLimit;
	public uint middleLoadLimit;
	public uint highLoadLimit;
	[ColumnIndex(9)][TsvArray(',')]
	public float[] areaCenterPos;
	[ColumnIndex(10)][TsvArray(',')]
	public float[] areaCenterRot;

	//not in client
	[ColumnIndex(7)]
	[TsvArray(',')]
	public float[] playerSafe;
	[ColumnIndex(8)]
	public uint highLoad;
	[ColumnIndex(11)]
	public string blueprintTransferPoint; //List<List<uint>>
}