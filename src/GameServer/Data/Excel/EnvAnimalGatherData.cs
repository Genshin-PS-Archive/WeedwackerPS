using Weedwacker.GameServer.Data.Enums;
using Weedwacker.GameServer.Data.Excel.Common;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(13)]
public class EnvAnimalGatherData
{
	[ColumnIndex(0)]
	public uint animalId;
	[ColumnIndex(2)]
	public EntityType entityType;
	[ColumnIndex(3)][TsvArray(3)]
	public IdCountConfig[] gatherItemList;
	[ColumnIndex(9)]
	public uint? escapeRadius;
	[ColumnIndex(10)]
	public uint? escapeTime;
	[ColumnIndex(11)]
	public uint? aliveTime;
	[ColumnIndex(12)]
	public string excludeWeathers; //array

	//not in client
	[ColumnIndex(1)]
	public uint areaID;
}
