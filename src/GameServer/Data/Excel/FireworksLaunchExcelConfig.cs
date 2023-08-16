using Weedwacker.GameServer.Data.Enums;

namespace Weedwacker.GameServer.Data.Excel;

[Columns(5)]
public class FireworksLaunchExcelConfig
{
	[ColumnIndex(0)]
	public FireworksLaunchParamType launchParamType;
	[ColumnIndex(1)]
	public int defaultValue;
	[ColumnIndex(2)][TsvArray(',')]
	public int[] adjustRange;
	[ColumnIndex(3)]
	public int adjustStep;
	[ColumnIndex(4)]
	public string launchParamNameText;
	//public uint launchParamNameTextMapHash;
}