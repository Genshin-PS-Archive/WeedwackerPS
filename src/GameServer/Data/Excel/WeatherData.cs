using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(12)]
public class WeatherData
{
	[ColumnIndex(0)]
	public uint areaID;
	[ColumnIndex(1)]
	public uint weatherAreaId;
	[ColumnIndex(2)]
	public string maxHeightStr;
	[ColumnIndex(4)]
	public uint? gadgetID;
	[ColumnIndex(5)]
	public bool isDefaultValid;
	[ColumnIndex(6)]
	public string templateName;
	[ColumnIndex(7)]
	public uint priority;
	[ColumnIndex(8)]
	public string profileName;
	[ColumnIndex(9)]
	public ClimateType defaultClimate;
	[ColumnIndex(10)]
	public bool isUseDefault;
	[ColumnIndex(11)]
	public uint sceneID;

	//not in client
	[ColumnIndex(3)]
	public string description;
}
